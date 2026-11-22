namespace GerenciamentoVeiculos.Domain.Interfaces.Messaging;

/// <summary>
/// Contrato para serviços de publicação e consumo de mensagens em fila (ex.: Azure Service Bus / Azure Queue Storage).
/// </summary>
public interface IFilaMensageriaService
{
    /// <summary>
    /// Envia uma mensagem serializada para a fila configurada.
    /// </summary>
    Task EnviarAsync<T>(T mensagem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Inicia a escuta ativa da fila, invocando o handler para cada mensagem processada.
    /// </summary>
    Task IniciarConsumoAsync<T>(Func<T, CancellationToken, Task> manipuladorMensagem, CancellationToken cancellationToken = default);
}
