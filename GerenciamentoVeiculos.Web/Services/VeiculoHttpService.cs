using System.Net.Http.Json;
using GerenciamentoVeiculos.Application.DTOs;

namespace GerenciamentoVeiculos.Web.Services;

public class VeiculoHttpService : IVeiculoHttpService
{
    private readonly HttpClient _http;
    private const string RotaBase = "api/v1/veiculos";

    public VeiculoHttpService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<VeiculoRespostaDto>> ObterComFiltroAsync(string? placa = null, string? modelo = null, string? nomeProprietario = null)
    {
        try
        {
            var queryParams = new List<string>();
            if (!string.IsNullOrWhiteSpace(placa))
                queryParams.Add($"placa={Uri.EscapeDataString(placa)}");
            if (!string.IsNullOrWhiteSpace(modelo))
                queryParams.Add($"modelo={Uri.EscapeDataString(modelo)}");
            if (!string.IsNullOrWhiteSpace(nomeProprietario))
                queryParams.Add($"nomeProprietario={Uri.EscapeDataString(nomeProprietario)}");

            var url = queryParams.Any()
                ? $"{RotaBase}?{string.Join("&", queryParams)}"
                : RotaBase;

            var retorno = await _http.GetFromJsonAsync<List<VeiculoRespostaDto>>(url);
            return retorno ?? new List<VeiculoRespostaDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao consultar veículos: {ex.Message}");
            return new List<VeiculoRespostaDto>();
        }
    }

    public async Task<VeiculoRespostaDto?> ObterPorIdAsync(Guid id)
    {
        try
        {
            return await _http.GetFromJsonAsync<VeiculoRespostaDto>($"{RotaBase}/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<(bool Sucesso, string? MensagemErro)> CriarAsync(VeiculoCriacaoDto dto)
    {
        var response = await _http.PostAsJsonAsync(RotaBase, dto);
        if (response.IsSuccessStatusCode)
            return (true, null);

        var erro = await HttpHelper.ExtrairMensagemErroAsync(response);
        return (false, erro);
    }

    public async Task<(bool Sucesso, string? MensagemErro)> AtualizarAsync(Guid id, VeiculoAtualizacaoDto dto)
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
