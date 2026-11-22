using FluentValidation;
using GerenciamentoVeiculos.Application.DTOs;
using GerenciamentoVeiculos.Domain.Validation;

namespace GerenciamentoVeiculos.Application.Validators;

public class ProprietarioCriacaoValidator : AbstractValidator<ProprietarioCriacaoDto>
{
    public ProprietarioCriacaoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O Nome do proprietário é obrigatório.")
            .Length(3, 100).WithMessage("O Nome deve conter entre 3 e 100 caracteres.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Must(ValidadorCpf.EhValido).WithMessage("O CPF informado é inválido de acordo com o cálculo dos dígitos verificadores.");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("A Data de Nascimento é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A Data de Nascimento não pode ser uma data futura.");
    }
}

public class ProprietarioAtualizacaoValidator : AbstractValidator<ProprietarioAtualizacaoDto>
{
    public ProprietarioAtualizacaoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O Nome do proprietário é obrigatório.")
            .Length(3, 100).WithMessage("O Nome deve conter entre 3 e 100 caracteres.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Must(ValidadorCpf.EhValido).WithMessage("O CPF informado é inválido de acordo com o cálculo dos dígitos verificadores.");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("A Data de Nascimento é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A Data de Nascimento não pode ser uma data futura.");
    }
}
