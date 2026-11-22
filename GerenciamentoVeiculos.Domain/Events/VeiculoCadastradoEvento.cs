namespace GerenciamentoVeiculos.Domain.Events;

/// <summary>
/// Evento de integração publicado na fila quando um novo veículo é cadastrado no sistema.
/// </summary>
public record VeiculoCadastradoEvento(
    Guid VeiculoId,
    string Placa,
    string Modelo,
    int Ano,
    Guid ProprietarioId,
    string NomeProprietario,
    DateTime DataOcorrencia);
