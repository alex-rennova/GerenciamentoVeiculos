using FluentValidation;
using GerenciamentoVeiculos.Application.DTOs;
using GerenciamentoVeiculos.Application.Interfaces;
using GerenciamentoVeiculos.Application.Mappings;
using GerenciamentoVeiculos.Domain.Exceptions;
using GerenciamentoVeiculos.Domain.Interfaces.Repositories;
using GerenciamentoVeiculos.Domain.Validation;

namespace GerenciamentoVeiculos.Application.Services;

public class ProprietarioService : IProprietarioService
{
    private readonly IProprietarioRepository _repository;
    private readonly IValidator<ProprietarioCriacaoDto> _criacaoValidator;
    private readonly IValidator<ProprietarioAtualizacaoDto> _atualizacaoValidator;

    public ProprietarioService(
        IProprietarioRepository repository,
        IValidator<ProprietarioCriacaoDto> criacaoValidator,
        IValidator<ProprietarioAtualizacaoDto> atualizacaoValidator)
    {
        _repository = repository;
        _criacaoValidator = criacaoValidator;
        _atualizacaoValidator = atualizacaoValidator;
    }

    public async Task<IEnumerable<ProprietarioRespostaDto>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        var proprietarios = await _repository.ObterTodosAsync(cancellationToken);
        return proprietarios.Select(p => p.ToDto(incluirVeiculos: true));
    }

    public async Task<ProprietarioRespostaDto> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var proprietario = await _repository.ObterPorIdAsync(id, incluirVeiculos: true, cancellationToken);
        if (proprietario == null)
            throw new RecursoNaoEncontradoException($"Proprietário com Id '{id}' não foi encontrado.");

        return proprietario.ToDto(incluirVeiculos: true);
    }

    public async Task<ProprietarioRespostaDto> CriarAsync(ProprietarioCriacaoDto dto, CancellationToken cancellationToken = default)
    {
        var validacao = await _criacaoValidator.ValidateAsync(dto, cancellationToken);
        if (!validacao.IsValid)
            throw new RegraNegocioException(string.Join(" ", validacao.Errors.Select(e => e.ErrorMessage)));

        var cpfLimpo = ValidadorCpf.LimparCpf(dto.Cpf);
        if (await _repository.ExisteCpfAsync(cpfLimpo, cancellationToken: cancellationToken))
            throw new ConflitoException($"Já existe um proprietário cadastrado com o CPF informado: {ValidadorCpf.Formatar(cpfLimpo)}.");

        var novoProprietario = dto.ToEntity();
        await _repository.AdicionarAsync(novoProprietario, cancellationToken);

        return novoProprietario.ToDto();
    }

    public async Task AtualizarAsync(Guid id, ProprietarioAtualizacaoDto dto, CancellationToken cancellationToken = default)
    {
        var validacao = await _atualizacaoValidator.ValidateAsync(dto, cancellationToken);
        if (!validacao.IsValid)
            throw new RegraNegocioException(string.Join(" ", validacao.Errors.Select(e => e.ErrorMessage)));

        var proprietario = await _repository.ObterPorIdAsync(id, cancellationToken: cancellationToken);
        if (proprietario == null)
            throw new RecursoNaoEncontradoException($"Proprietário com Id '{id}' não foi encontrado.");

        var cpfLimpo = ValidadorCpf.LimparCpf(dto.Cpf);
        if (await _repository.ExisteCpfAsync(cpfLimpo, idIgnorar: id, cancellationToken: cancellationToken))
            throw new ConflitoException($"Já existe outro proprietário cadastrado com o CPF informado: {ValidadorCpf.Formatar(cpfLimpo)}.");

        proprietario.AtualizarDados(dto.Nome, dto.Cpf, dto.DataNascimento);
        await _repository.AtualizarAsync(proprietario, cancellationToken);
    }

    public async Task RemoverAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var proprietario = await _repository.ObterPorIdAsync(id, cancellationToken: cancellationToken);
        if (proprietario == null)
            throw new RecursoNaoEncontradoException($"Proprietário com Id '{id}' não foi encontrado.");

        // Regra de negócio obrigatória: Impedir a exclusão de proprietário caso ele tenha veículos cadastrados.
        var possuiVeiculos = await _repository.PossuiVeiculosVinculadosAsync(id, cancellationToken);
        if (possuiVeiculos)
            throw new ConflitoException($"Não é permitido excluir o proprietário '{proprietario.Nome}', pois existem veículos cadastrados vinculados a ele.");

        await _repository.RemoverAsync(proprietario, cancellationToken);
    }
}
