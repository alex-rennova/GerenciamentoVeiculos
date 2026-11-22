using GerenciamentoVeiculos.Domain.Exceptions;
using GerenciamentoVeiculos.Domain.Validation;

namespace GerenciamentoVeiculos.Domain.Entities;

/// <summary>
/// Entidade de domínio que representa o veículo vinculado a um proprietário.
/// </summary>
public class Veiculo
{
    public Guid Id { get; private set; }
    public string Placa { get; private set; } = string.Empty;
    public string Modelo { get; private set; } = string.Empty;
    public int Ano { get; private set; }
    public Guid ProprietarioId { get; private set; }

    // Propriedade de navegação
    public virtual Proprietario? Proprietario { get; private set; }

    // Construtor sem parâmetros para uso do EF Core
    protected Veiculo() { }

    public Veiculo(string placa, string modelo, int ano, Guid proprietarioId, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        ProprietarioId = proprietarioId;
        AtualizarDados(placa, modelo, ano);
    }

    public void AtualizarDados(string placa, string modelo, int ano)
    {
        if (!ValidadorPlaca.EhValida(placa))
            throw new RegraNegocioException("A Placa informada é inválida. Utilize o formato ABC-1234 ou o padrão Mercosul.");

        if (string.IsNullOrWhiteSpace(modelo))
            throw new RegraNegocioException("O Modelo do veículo é obrigatório.");

        if (ano <= 1980)
            throw new RegraNegocioException("O Ano do veículo deve ser maior que 1980.");

        var anoMaximoPermitido = DateTime.UtcNow.Year + 1;
        if (ano > anoMaximoPermitido)
            throw new RegraNegocioException($"O Ano do veículo não pode ultrapassar {anoMaximoPermitido}.");

        Placa = ValidadorPlaca.Padronizar(placa);
        Modelo = modelo.Trim();
        Ano = ano;
    }

    public void AlterarProprietario(Guid novoProprietarioId)
    {
        if (novoProprietarioId == Guid.Empty)
            throw new RegraNegocioException("O identificador do Proprietário é obrigatório.");

        ProprietarioId = novoProprietarioId;
    }
}
