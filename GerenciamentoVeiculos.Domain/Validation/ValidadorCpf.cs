using System.Text.RegularExpressions;

namespace GerenciamentoVeiculos.Domain.Validation;

/// <summary>
/// Helper de validação de CPF de acordo com o algoritmo oficial dos dígitos verificadores.
/// </summary>
public static class ValidadorCpf
{
    private static readonly Regex ApenasDigitosRegex = new(@"[^\d]", RegexOptions.Compiled);

    /// <summary>
    /// Limpa pontuações mantendo apenas os dígitos numéricos.
    /// </summary>
    public static string LimparCpf(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return string.Empty;

        return ApenasDigitosRegex.Replace(cpf, string.Empty);
    }

    /// <summary>
    /// Valida se o CPF é válido conforme regras de tamanho, repetição e dígitos verificadores.
    /// </summary>
    public static bool EhValido(string? cpf)
    {
        var cpfLimpo = LimparCpf(cpf);

        if (cpfLimpo.Length != 11)
            return false;

        // Rejeita padrões com todos os números iguais (ex: 111.111.111-11)
        if (cpfLimpo.Distinct().Count() == 1)
            return false;

        // Cálculo do primeiro dígito verificador
        var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var soma = 0;
        for (var i = 0; i < 9; i++)
            soma += int.Parse(cpfLimpo[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        var primeiroDigito = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpfLimpo[9].ToString()) != primeiroDigito)
            return false;

        // Cálculo do segundo dígito verificador
        var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        soma = 0;
        for (var i = 0; i < 10; i++)
            soma += int.Parse(cpfLimpo[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        var segundoDigito = resto < 2 ? 0 : 11 - resto;

        return int.Parse(cpfLimpo[10].ToString()) == segundoDigito;
    }

    /// <summary>
    /// Formata um CPF numérico para o padrão 000.000.000-00.
    /// </summary>
    public static string Formatar(string? cpf)
    {
        var limpo = LimparCpf(cpf);
        if (limpo.Length != 11)
            return cpf ?? string.Empty;

        return Convert.ToUInt64(limpo).ToString(@"000\.000\.000\-00");
    }
}
