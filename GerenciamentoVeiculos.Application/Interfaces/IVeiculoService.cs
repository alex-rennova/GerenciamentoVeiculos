using GerenciamentoVeiculos.Application.DTOs;

namespace GerenciamentoVeiculos.Application.Interfaces;

public interface IVeiculoService
{
    Task<IEnumerable<VeiculoRespostaDto>> ObterComFiltroAsync(VeiculoFiltroDto filtro, CancellationToken cancellationToken = default);
    Task<VeiculoRespostaDto> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<VeiculoRespostaDto> CriarAsync(VeiculoCriacaoDto dto, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Guid id, VeiculoAtualizacaoDto dto, CancellationToken cancellationToken = default);
    Task RemoverAsync(Guid id, CancellationToken cancellationToken = default);
}
