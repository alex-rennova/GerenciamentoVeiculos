using FluentValidation;
using GerenciamentoVeiculos.Application.DTOs;
using GerenciamentoVeiculos.Domain.Validation;

namespace GerenciamentoVeiculos.Application.Validators;

public class VeiculoCriacaoValidator : AbstractValidator<VeiculoCriacaoDto>
{
    public VeiculoCriacaoValidator()
    {
        RuleFor(x => x.Placa)
            .NotEmpty().WithMessage("A Placa do veículo é obrigatória.")
            .Must(ValidadorPlaca.EhValida).WithMessage("A Placa deve possuir formato válido (ex: ABC-1234 ou padrão Mercosul ABC1D23).");

        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("O Modelo do veículo é obrigatório.")
            .MaximumLength(80).WithMessage("O Modelo deve ter no máximo 80 caracteres.");

        RuleFor(x => x.Ano)
            .GreaterThan(1980).WithMessage("O Ano do veículo deve ser maior que 1980.")
            .LessThanOrEqualTo(DateTime.UtcNow.Year + 1)
            .WithMessage($"O Ano do veículo não pode ser superior a {DateTime.UtcNow.Year + 1}.");

        RuleFor(x => x.ProprietarioId)
            .NotEmpty().WithMessage("O ProprietarioId é obrigatório e não pode ser vazio.");
    }
}

public class VeiculoAtualizacaoValidator : AbstractValidator<VeiculoAtualizacaoDto>
{
    public VeiculoAtualizacaoValidator()
    {
        RuleFor(x => x.Placa)
            .NotEmpty().WithMessage("A Placa do veículo é obrigatória.")
            .Must(ValidadorPlaca.EhValida).WithMessage("A Placa deve possuir formato válido (ex: ABC-1234 ou padrão Mercosul ABC1D23).");

        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("O Modelo do veículo é obrigatório.")
            .MaximumLength(80).WithMessage("O Modelo deve ter no máximo 80 caracteres.");

        RuleFor(x => x.Ano)
            .GreaterThan(1980).WithMessage("O Ano do veículo deve ser maior que 1980.")
            .LessThanOrEqualTo(DateTime.UtcNow.Year + 1)
            .WithMessage($"O Ano do veículo não pode ser superior a {DateTime.UtcNow.Year + 1}.");

        RuleFor(x => x.ProprietarioId)
            .NotEmpty().WithMessage("O ProprietarioId é obrigatório e não pode ser vazio.");
    }
}
