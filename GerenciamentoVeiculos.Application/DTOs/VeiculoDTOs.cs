namespace GerenciamentoVeiculos.Application.DTOs;

/// <summary>
/// Dados necessários para cadastrar um novo veículo.
/// </summary>
public record VeiculoCriacaoDto(
    string Placa,
    string Modelo,
    int Ano,
    Guid ProprietarioId);

/// <summary>
/// Dados para atualizar as informações de um veículo.
/// </summary>
public record VeiculoAtualizacaoDto(
    string Placa,
    string Modelo,
    int Ano,
    Guid ProprietarioId);

/// <summary>
/// Retorno detalhado do veículo incluindo informações de seu proprietário.
/// </summary>
public record VeiculoRespostaDto(
    Guid Id,
    string Placa,
    string Modelo,
    int Ano,
    Guid ProprietarioId,
    string NomeProprietario);

/// <summary>
/// Parâmetros de consulta para busca flexível de veículos.
/// </summary>
public record VeiculoFiltroDto(
    string? Placa = null,
    string? Modelo = null,
    string? NomeProprietario = null);
