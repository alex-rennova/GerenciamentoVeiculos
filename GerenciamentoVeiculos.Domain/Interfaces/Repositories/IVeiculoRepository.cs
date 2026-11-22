using GerenciamentoVeiculos.Domain.Entities;

namespace GerenciamentoVeiculos.Domain.Interfaces.Repositories;

/// <summary>
/// Contrato de persistência para a entidade Veículo com suporte a consultas filtradas.
/// </summary>
public interface IVeiculoRepository
{
    Task<IEnumerable<Veiculo>> ObterComFiltroAsync(
        string? placa = null,
        string? modelo = null,
        string? nomeProprietario = null,
        CancellationToken cancellationToken = default);

    Task<Veiculo?> ObterPorIdAsync(Guid id, bool incluirProprietario = true, CancellationToken cancellationToken = default);
    Task<Veiculo?> ObterPorPlacaAsync(string placa, CancellationToken cancellationToken = default);
    Task<bool> ExistePlacaAsync(string placa, Guid? idIgnorar = null, CancellationToken cancellationToken = default);
    Task AdicionarAsync(Veiculo veiculo, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Veiculo veiculo, CancellationToken cancellationToken = default);
    Task RemoverAsync(Veiculo veiculo, CancellationToken cancellationToken = default);
}
