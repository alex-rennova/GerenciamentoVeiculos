using GerenciamentoVeiculos.Application.DTOs;

namespace GerenciamentoVeiculos.Application.Interfaces;

public interface IProprietarioService
{
    Task<IEnumerable<ProprietarioRespostaDto>> ObterTodosAsync(CancellationToken cancellationToken = default);
    Task<ProprietarioRespostaDto> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProprietarioRespostaDto> CriarAsync(ProprietarioCriacaoDto dto, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Guid id, ProprietarioAtualizacaoDto dto, CancellationToken cancellationToken = default);
    Task RemoverAsync(Guid id, CancellationToken cancellationToken = default);
}
