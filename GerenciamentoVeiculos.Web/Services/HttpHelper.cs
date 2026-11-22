using System.Net.Http.Json;
using System.Text.Json;

namespace GerenciamentoVeiculos.Web.Services;

public static class HttpHelper
{
    public static async Task<string> ExtrairMensagemErroAsync(HttpResponseMessage response)
    {
        try
        {
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json))
                return $"Erro na operação (HTTP {(int)response.StatusCode}: {response.ReasonPhrase})";

            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("detail", out var detailProp) && !string.IsNullOrWhiteSpace(detailProp.GetString()))
            {
                return detailProp.GetString()!;
            }

            if (doc.RootElement.TryGetProperty("title", out var titleProp) && !string.IsNullOrWhiteSpace(titleProp.GetString()))
            {
                return titleProp.GetString()!;
            }

            return json;
        }
        catch
        {
            return $"Falha na comunicação com o servidor (HTTP {(int)response.StatusCode}).";
        }
    }
}
