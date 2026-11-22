namespace GerenciamentoVeiculos.Infrastructure.Messaging;

public class AzureServiceBusConfig
{
    public const string SectionName = "AzureServiceBus";

    public string ConnectionString { get; set; } = string.Empty;
    public string NomeFila { get; set; } = "fila-veiculos-cadastrados";
    public bool UsarSimuladorEmMemoria { get; set; } = true;
}
