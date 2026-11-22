using GerenciamentoVeiculos.Application.DTOs;

namespace GerenciamentoVeiculos.Web.Services;

public interface IProprietarioHttpService
{
    Task<List<ProprietarioRespostaDto>> ObterTodosAsync();
    Task<ProprietarioRespostaDto?> ObterPorIdAsync(Guid id);
    Task<(bool Sucesso, string? MensagemErro)> CriarAsync(ProprietarioCriacaoDto dto);
    Task<(bool Sucesso, string? MensagemErro)> AtualizarAsync(Guid id, ProprietarioAtualizacaoDto dto);
    Task<(bool Sucesso, string? MensagemErro)> ExcluirAsync(Guid id);
}
