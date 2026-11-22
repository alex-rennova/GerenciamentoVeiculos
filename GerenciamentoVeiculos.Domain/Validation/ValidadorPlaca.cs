using System.Text.RegularExpressions;

namespace GerenciamentoVeiculos.Domain.Validation;

/// <summary>
/// Helper de validação de Placa de veículos brasileiros.
/// Dá suporte tanto ao padrão tradicional (ABC-1234) quanto ao padrão Mercosul (ABC1D23).
/// </summary>
public static class ValidadorPlaca
{
    // Formato Tradicional: 3 letras, hífen opcional, 4 números (ex: ABC-1234 ou ABC1234)
    private static readonly Regex PlacaTradicionalRegex = new(@"^[a-zA-Z]{3}-?[0-9]{4}$", RegexOptions.Compiled);

    // Formato Mercosul: 3 letras, 1 número, 1 letra, 2 números (ex: ABC1D23)
    private static readonly Regex PlacaMercosulRegex = new(@"^[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}$", RegexOptions.Compiled);

    /// <summary>
    /// Valida se a placa atende ao padrão brasileiro (Tradicional ou Mercosul).
    /// </summary>
    public static bool EhValida(string? placa)
    {
        if (string.IsNullOrWhiteSpace(placa))
            return false;

        var placaLimpa = placa.Trim();
        return PlacaTradicionalRegex.IsMatch(placaLimpa) || PlacaMercosulRegex.IsMatch(placaLimpa);
    }

    /// <summary>
    /// Padroniza a placa em letras maiúsculas e adiciona hífen se for formato tradicional sem hífen.
    /// </summary>
    public static string Padronizar(string? placa)
    {
        if (string.IsNullOrWhiteSpace(placa))
            return string.Empty;

        var limpa = placa.Trim().ToUpperInvariant();

        // Se for ABC1234 tradicional sem hífen, formata para ABC-1234
        if (Regex.IsMatch(limpa, @"^[A-Z]{3}[0-9]{4}$"))
        {
            return $"{limpa.Substring(0, 3)}-{limpa.Substring(3, 4)}";
        }

        return limpa;
    }
}
