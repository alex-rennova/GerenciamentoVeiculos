using System.Text.Json;
using System.Threading.Channels;
using Azure.Messaging.ServiceBus;
using GerenciamentoVeiculos.Domain.Interfaces.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GerenciamentoVeiculos.Infrastructure.Messaging;

/// <summary>
/// Serviço de mensageria compatível com Azure Service Bus.
/// Possui fallback transparente para fila em memória via Channels caso a Connection String não seja informada.
/// </summary>
public class AzureServiceBusFilaService : IFilaMensageriaService, IAsyncDisposable
{
    private readonly AzureServiceBusConfig _config;
    private readonly ILogger<AzureServiceBusFilaService> _logger;
    private readonly ServiceBusClient? _client;
    private readonly ServiceBusSender? _sender;
    private ServiceBusProcessor? _processor;

    // Fila em memória para modo local / simulação em live-code
    private static readonly Channel<string> CanalEmMemoria = Channel.CreateUnbounded<string>();

    public AzureServiceBusFilaService(
        IOptions<AzureServiceBusConfig> config,
        ILogger<AzureServiceBusFilaService> logger)
    {
        _config = config.Value;
        _logger = logger;

        var temConexaoReal = !string.IsNullOrWhiteSpace(_config.ConnectionString) &&
                             !_config.ConnectionString.Contains("<SUA_AZURE_") &&
                             !_config.UsarSimuladorEmMemoria;

        if (temConexaoReal)
        {
            try
            {
                _client = new ServiceBusClient(_config.ConnectionString);
                _sender = _client.CreateSender(_config.NomeFila);
                _logger.LogInformation("Conectado com sucesso ao Azure Service Bus na fila '{Fila}'.", _config.NomeFila);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Falha ao inicializar conexão com Azure Service Bus. Alternando para o modo simulado em memória.");
            }
        }
        else
        {
            _logger.LogInformation("Modo Simulação ativado para fila Azure Service Bus: '{Fila}'. (Mensagens serão roteadas via Channel local)", _config.NomeFila);
        }
    }

    public async Task EnviarAsync<T>(T mensagem, CancellationToken cancellationToken = default)
    {
        var corpoJson = JsonSerializer.Serialize(mensagem, new JsonSerializerOptions { WriteIndented = false });

        if (_sender != null)
        {
            try
            {
                var serviceBusMessage = new ServiceBusMessage(corpoJson)
                {
                    ContentType = "application/json",
                    Subject = typeof(T).Name,
                    MessageId = Guid.NewGuid().ToString()
                };

                await _sender.SendMessageAsync(serviceBusMessage, cancellationToken);
                _logger.LogInformation("[AZURE SERVICE BUS] Mensagem do tipo '{Tipo}' enviada com sucesso para a fila '{Fila}'.", typeof(T).Name, _config.NomeFila);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar mensagem para o Azure Service Bus real. Salvando no canal em memória.");
            }
        }

        // Fallback / Modo Simulado
        await CanalEmMemoria.Writer.WriteAsync(corpoJson, cancellationToken);
        _logger.LogInformation("[SIMULADOR AZURE QUEUE] Mensagem enviada para a fila virtual '{Fila}': {Conteudo}", _config.NomeFila, corpoJson);
    }

    public async Task IniciarConsumoAsync<T>(Func<T, CancellationToken, Task> manipuladorMensagem, CancellationToken cancellationToken = default)
    {
        if (_client != null)
        {
            try
            {
                _processor = _client.CreateProcessor(_config.NomeFila, new ServiceBusProcessorOptions
                {
                    AutoCompleteMessages = false,
                    MaxConcurrentCalls = 1
                });

                _processor.ProcessMessageAsync += async args =>
                {
                    var corpoTexto = args.Message.Body.ToString();
                    var obj = JsonSerializer.Deserialize<T>(corpoTexto);
                    if (obj != null)
                    {
                        await manipuladorMensagem(obj, args.CancellationToken);
                    }
                    await args.CompleteMessageAsync(args.Message);
                };

                _processor.ProcessErrorAsync += args =>
                {
                    _logger.LogError(args.Exception, "[AZURE SERVICE BUS] Erro durante o processamento da mensagem.");
                    return Task.CompletedTask;
                };

                await _processor.StartProcessingAsync(cancellationToken);
                _logger.LogInformation("[AZURE SERVICE BUS] Escuta ativa iniciada na fila '{Fila}'.", _config.NomeFila);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao iniciar processador do Azure Service Bus real. Escutando canal em memória.");
            }
        }

        // Processamento contínuo em memória para modo simulado / live code
        _ = Task.Run(async () =>
        {
            var reader = CanalEmMemoria.Reader;
            while (await reader.WaitToReadAsync(cancellationToken))
            {
                while (reader.TryRead(out var itemJson))
                {
                    try
                    {
                        var obj = JsonSerializer.Deserialize<T>(itemJson);
                        if (obj != null)
                        {
                            await manipuladorMensagem(obj, cancellationToken);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "[SIMULADOR AZURE QUEUE] Erro ao processar mensagem do canal em memória.");
                    }
                }
            }
        }, cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        if (_processor != null)
        {
            await _processor.DisposeAsync();
        }

        if (_sender != null)
        {
            await _sender.DisposeAsync();
        }

        if (_client != null)
        {
            await _client.DisposeAsync();
        }

        GC.SuppressFinalize(this);
    }
}
