namespace GerenciamentoVeiculos.Application.DTOs;

/// <summary>
/// Dados necessários para criação de um novo proprietário.
/// </summary>
public record ProprietarioCriacaoDto(
    string Nome,
    string Cpf,
    DateTime DataNascimento);

/// <summary>
/// Dados para atualização cadastral de um proprietário.
/// </summary>
public record ProprietarioAtualizacaoDto(
    string Nome,
    string Cpf,
    DateTime DataNascimento);

/// <summary>
/// Retorno detalhado de um proprietário.
/// </summary>
public record ProprietarioRespostaDto(
    Guid Id,
    string Nome,
    string Cpf,
    string CpfFormatado,
    DateTime DataNascimento,
    int QuantidadeVeiculos,
    IEnumerable<VeiculoResumoDto>? Veiculos = null);

/// <summary>
/// Visão simplificada de veículo para exibição dentro do proprietário.
/// </summary>
public record VeiculoResumoDto(
    Guid Id,
    string Placa,
    string Modelo,
    int Ano);
