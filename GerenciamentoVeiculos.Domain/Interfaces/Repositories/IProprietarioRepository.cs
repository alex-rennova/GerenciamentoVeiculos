using GerenciamentoVeiculos.Domain.Entities;

namespace GerenciamentoVeiculos.Domain.Interfaces.Repositories;

/// <summary>
/// Contrato de persistência para a entidade Proprietário.
/// </summary>
public interface IProprietarioRepository
{
    Task<IEnumerable<Proprietario>> ObterTodosAsync(CancellationToken cancellationToken = default);
    Task<Proprietario?> ObterPorIdAsync(Guid id, bool incluirVeiculos = false, CancellationToken cancellationToken = default);
    Task<Proprietario?> ObterPorCpfAsync(string cpf, CancellationToken cancellationToken = default);
    Task<bool> ExisteCpfAsync(string cpf, Guid? idIgnorar = null, CancellationToken cancellationToken = default);
    Task<bool> PossuiVeiculosVinculadosAsync(Guid proprietarioId, CancellationToken cancellationToken = default);
    Task AdicionarAsync(Proprietario proprietario, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Proprietario proprietario, CancellationToken cancellationToken = default);
    Task RemoverAsync(Proprietario proprietario, CancellationToken cancellationToken = default);
}
