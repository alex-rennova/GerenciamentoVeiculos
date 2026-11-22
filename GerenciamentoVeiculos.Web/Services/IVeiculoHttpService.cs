using GerenciamentoVeiculos.Application.DTOs;

namespace GerenciamentoVeiculos.Web.Services;

public interface IVeiculoHttpService
{
    Task<List<VeiculoRespostaDto>> ObterComFiltroAsync(string? placa = null, string? modelo = null, string? nomeProprietario = null);
    Task<VeiculoRespostaDto?> ObterPorIdAsync(Guid id);
    Task<(bool Sucesso, string? MensagemErro)> CriarAsync(VeiculoCriacaoDto dto);
    Task<(bool Sucesso, string? MensagemErro)> AtualizarAsync(Guid id, VeiculoAtualizacaoDto dto);
    Task<(bool Sucesso, string? MensagemErro)> ExcluirAsync(Guid id);
}
