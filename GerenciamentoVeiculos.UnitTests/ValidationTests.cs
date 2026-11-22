using GerenciamentoVeiculos.Domain.Validation;
using Xunit;

namespace GerenciamentoVeiculos.UnitTests;

public class ValidationTests
{
    [Theory]
    [InlineData("12345678909", true)]   // CPF matematicamente válido
    [InlineData("98765432100", true)]   // CPF matematicamente válido
    [InlineData("52998224725", true)]   // CPF matematicamente válido
    [InlineData("11111111111", false)]  // Números todos iguais (deve ser rejeitado)
    [InlineData("12345678900", false)]  // Dígitos verificadores incorretos
    [InlineData("123", false)]          // Tamanho insuficiente
    [InlineData("", false)]             // Vazio
    [InlineData(null, false)]           // Nulo
    public void ValidadorCpf_DeveValidarCorretamente(string? cpf, bool esperado)
    {
        var resultado = ValidadorCpf.EhValido(cpf);
        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData("ABC-1234", true)]  // Tradicional com hífen
    [InlineData("abc1234", true)]   // Tradicional sem hífen minúsculo
    [InlineData("ABC1D23", true)]   // Mercosul
    [InlineData("xyz9a88", true)]   // Mercosul minúsculo
    [InlineData("AB1234", false)]   // Letras insuficientes
    [InlineData("ABCD-1234", false)]// Letras em excesso
    [InlineData("", false)]         // Vazio
    [InlineData(null, false)]       // Nulo
    public void ValidadorPlaca_DeveValidarCorretamente(string? placa, bool esperado)
    {
        var resultado = ValidadorPlaca.EhValida(placa);
        Assert.Equal(esperado, resultado);
    }
}
