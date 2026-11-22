using GerenciamentoVeiculos.Domain.Exceptions;
using GerenciamentoVeiculos.Domain.Validation;

namespace GerenciamentoVeiculos.Domain.Entities;

/// <summary>
/// Entidade de domínio que representa o proprietário de veículos.
/// </summary>
public class Proprietario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public DateTime DataNascimento { get; private set; }

    // Relacionamento 1:N com Veículos
    public virtual ICollection<Veiculo> Veiculos { get; private set; } = new List<Veiculo>();

    // Construtor sem parâmetros para uso do EF Core
    protected Proprietario() { }

    public Proprietario(string nome, string cpf, DateTime dataNascimento, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        AtualizarDados(nome, cpf, dataNascimento);
    }

    public void AtualizarDados(string nome, string cpf, DateTime dataNascimento)
    {
        if (string.IsNullOrWhiteSpace(nome) || nome.Trim().Length < 3 || nome.Trim().Length > 100)
            throw new RegraNegocioException("O Nome do proprietário é obrigatório e deve ter entre 3 e 100 caracteres.");

        var cpfLimpo = ValidadorCpf.LimparCpf(cpf);
        if (!ValidadorCpf.EhValido(cpfLimpo))
            throw new RegraNegocioException("O CPF informado é inválido.");

        if (dataNascimento > DateTime.UtcNow)
            throw new RegraNegocioException("A Data de Nascimento não pode ser uma data futura.");

        Nome = nome.Trim();
        Cpf = cpfLimpo;
        DataNascimento = dataNascimento.Date;
    }
}
