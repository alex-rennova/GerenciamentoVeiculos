using GerenciamentoVeiculos.Domain.Events;
using GerenciamentoVeiculos.Domain.Interfaces.Messaging;

namespace GerenciamentoVeiculos.Api.Workers;

/// <summary>
/// Background Worker (IHostedService) que roda acoplado ao ciclo de vida da API
/// para escutar e processar as mensagens da fila da Azure em tempo real.
/// </summary>
public class ProcessadorFilaWorker : BackgroundService
{
    private readonly IFilaMensageriaService _filaMensageriaService;
    private readonly ILogger<ProcessadorFilaWorker> _logger;

    public ProcessadorFilaWorker(
        IFilaMensageriaService filaMensageriaService,
        ILogger<ProcessadorFilaWorker> logger)
    {
        _filaMensageriaService = filaMensageriaService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(">>> [WORKER] Background Service de consumo de mensagens da fila inicializado.");

        // Inicia a escuta ativa da fila
        await _filaMensageriaService.IniciarConsumoAsync<VeiculoCadastradoEvento>(async (evento, ct) =>
        {
            _logger.LogInformation("========================================================================");
            _logger.LogInformation(">>> [WORKER - EVENTO RECEBIDO DA FILA DA AZURE]");
            _logger.LogInformation("    VeiculoId: {VeiculoId}", evento.VeiculoId);
            _logger.LogInformation("    Placa: {Placa}", evento.Placa);
            _logger.LogInformation("    Modelo: {Modelo} ({Ano})", evento.Modelo, evento.Ano);
            _logger.LogInformation("    Proprietário: {Nome} (Id: {ProprietarioId})", evento.NomeProprietario, evento.ProprietarioId);
            _logger.LogInformation("    Ocorrência: {Data:dd/MM/yyyy HH:mm:ss} UTC", evento.DataOcorrencia);
            _logger.LogInformation("========================================================================");

            // Simula processamento assíncrono (ex: emissão de apólice, notificação por e-mail, etc.)
            await Task.Delay(50, ct);

        }, stoppingToken);

        // Mantém o worker ativo até solicitação de cancelamento da aplicação
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        _logger.LogInformation(">>> [WORKER] Background Service de consumo finalizado.");
    }
}
