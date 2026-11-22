using System.Net.Http.Json;
using GerenciamentoVeiculos.Application.DTOs;

namespace GerenciamentoVeiculos.Web.Services;

public class ProprietarioHttpService : IProprietarioHttpService
{
    private readonly HttpClient _http;
    private const string RotaBase = "api/v1/proprietarios";

    public ProprietarioHttpService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProprietarioRespostaDto>> ObterTodosAsync()
    {
        try
        {
            var retorno = await _http.GetFromJsonAsync<List<ProprietarioRespostaDto>>(RotaBase);
            return retorno ?? new List<ProprietarioRespostaDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter proprietários: {ex.Message}");
            return new List<ProprietarioRespostaDto>();
        }
    }

    public async Task<ProprietarioRespostaDto?> ObterPorIdAsync(Guid id)
    {
        try
        {
            return await _http.GetFromJsonAsync<ProprietarioRespostaDto>($"{RotaBase}/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<(bool Sucesso, string? MensagemErro)> CriarAsync(ProprietarioCriacaoDto dto)
    {
        var response = await _http.PostAsJsonAsync(RotaBase, dto);
        if (response.IsSuccessStatusCode)
            return (true, null);

        var erro = await HttpHelper.ExtrairMensagemErroAsync(response);
        return (false, erro);
    }

    public async Task<(bool Sucesso, string? MensagemErro)> AtualizarAsync(Guid id, ProprietarioAtualizacaoDto dto)
    {
        var response = await _http.PutAsJsonAsync($"{RotaBase}/{id}", dto);
        if (response.IsSuccessStatusCode)
            return (true, null);

        var erro = await HttpHelper.ExtrairMensagemErroAsync(response);
        return (false, erro);
    }

    public async Task<(bool Sucesso, string? MensagemErro)> ExcluirAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"{RotaBase}/{id}");
        if (response.IsSuccessStatusCode)
            return (true, null);

        var erro = await HttpHelper.ExtrairMensagemErroAsync(response);
        return (false, erro);
    }
}
