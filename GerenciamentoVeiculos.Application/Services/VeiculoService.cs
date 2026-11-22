using FluentValidation;
using GerenciamentoVeiculos.Application.DTOs;
using GerenciamentoVeiculos.Application.Interfaces;
using GerenciamentoVeiculos.Application.Mappings;
using GerenciamentoVeiculos.Domain.Entities;
using GerenciamentoVeiculos.Domain.Events;
using GerenciamentoVeiculos.Domain.Exceptions;
using GerenciamentoVeiculos.Domain.Interfaces.Messaging;
using GerenciamentoVeiculos.Domain.Interfaces.Repositories;
using GerenciamentoVeiculos.Domain.Validation;

namespace GerenciamentoVeiculos.Application.Services;

public class VeiculoService : IVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IProprietarioRepository _proprietarioRepository;
    private readonly IFilaMensageriaService _filaMensageriaService;
    private readonly IValidator<VeiculoCriacaoDto> _criacaoValidator;
    private readonly IValidator<VeiculoAtualizacaoDto> _atualizacaoValidator;

    public VeiculoService(
        IVeiculoRepository veiculoRepository,
        IProprietarioRepository proprietarioRepository,
        IFilaMensageriaService filaMensageriaService,
        IValidator<VeiculoCriacaoDto> criacaoValidator,
        IValidator<VeiculoAtualizacaoDto> atualizacaoValidator)
    {
        _veiculoRepository = veiculoRepository;
        _proprietarioRepository = proprietarioRepository;
        _filaMensageriaService = filaMensageriaService;
        _criacaoValidator = criacaoValidator;
        _atualizacaoValidator = atualizacaoValidator;
    }

    public async Task<IEnumerable<VeiculoRespostaDto>> ObterComFiltroAsync(VeiculoFiltroDto filtro, CancellationToken cancellationToken = default)
    {
        var veiculos = await _veiculoRepository.ObterComFiltroAsync(
            placa: filtro.Placa,
            modelo: filtro.Modelo,
            nomeProprietario: filtro.NomeProprietario,
            cancellationToken: cancellationToken);

        return veiculos.Select(v => v.ToDto());
    }

    public async Task<VeiculoRespostaDto> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var veiculo = await _veiculoRepository.ObterPorIdAsync(id, incluirProprietario: true, cancellationToken);
        if (veiculo == null)
            throw new RecursoNaoEncontradoException($"Veículo com Id '{id}' não foi encontrado.");

        return veiculo.ToDto();
    }

    public async Task<VeiculoRespostaDto> CriarAsync(VeiculoCriacaoDto dto, CancellationToken cancellationToken = default)
    {
        var validacao = await _criacaoValidator.ValidateAsync(dto, cancellationToken);
        if (!validacao.IsValid)
            throw new RegraNegocioException(string.Join(" ", validacao.Errors.Select(e => e.ErrorMessage)));

        // Regra de negócio obrigatória: Validar se o proprietário existe
        var proprietario = await _proprietarioRepository.ObterPorIdAsync(dto.ProprietarioId, cancellationToken: cancellationToken);
        if (proprietario == null)
            throw new RecursoNaoEncontradoException($"Proprietário com Id '{dto.ProprietarioId}' não existe no sistema. Não é possível vincular o veículo.");

        var placaPadronizada = ValidadorPlaca.Padronizar(dto.Placa);
        if (await _veiculoRepository.ExistePlacaAsync(placaPadronizada, cancellationToken: cancellationToken))
            throw new ConflitoException($"Já existe um veículo cadastrado com a placa '{placaPadronizada}'.");

        var veiculo = new Veiculo(placaPadronizada, dto.Modelo, dto.Ano, dto.ProprietarioId);
        await _veiculoRepository.AdicionarAsync(veiculo, cancellationToken);

        // Disparo assíncrono para a Fila da Azure
        var evento = new VeiculoCadastradoEvento(
            VeiculoId: veiculo.Id,
            Placa: veiculo.Placa,
            Modelo: veiculo.Modelo,
            Ano: veiculo.Ano,
            ProprietarioId: proprietario.Id,
            NomeProprietario: proprietario.Nome,
            DataOcorrencia: DateTime.UtcNow);

        await _filaMensageriaService.EnviarAsync(evento, cancellationToken);

        // Recarrega para incluir o nome do proprietário no DTO de resposta
        var veiculoCriado = await _veiculoRepository.ObterPorIdAsync(veiculo.Id, incluirProprietario: true, cancellationToken);
        return (veiculoCriado ?? veiculo).ToDto();
    }

    public async Task AtualizarAsync(Guid id, VeiculoAtualizacaoDto dto, CancellationToken cancellationToken = default)
    {
        var validacao = await _atualizacaoValidator.ValidateAsync(dto, cancellationToken);
        if (!validacao.IsValid)
            throw new RegraNegocioException(string.Join(" ", validacao.Errors.Select(e => e.ErrorMessage)));

        var veiculo = await _veiculoRepository.ObterPorIdAsync(id, cancellationToken: cancellationToken);
        if (veiculo == null)
            throw new RecursoNaoEncontradoException($"Veículo com Id '{id}' não foi encontrado.");

        // Se o proprietário estiver sendo alterado, valida se o novo existe
        if (veiculo.ProprietarioId != dto.ProprietarioId)
        {
            var novoProprietario = await _proprietarioRepository.ObterPorIdAsync(dto.ProprietarioId, cancellationToken: cancellationToken);
            if (novoProprietario == null)
                throw new RecursoNaoEncontradoException($"Novo proprietário com Id '{dto.ProprietarioId}' não existe.");

            veiculo.AlterarProprietario(dto.ProprietarioId);
        }

        var placaPadronizada = ValidadorPlaca.Padronizar(dto.Placa);
        if (await _veiculoRepository.ExistePlacaAsync(placaPadronizada, idIgnorar: id, cancellationToken: cancellationToken))
            throw new ConflitoException($"Já existe outro veículo cadastrado com a placa '{placaPadronizada}'.");

        veiculo.AtualizarDados(placaPadronizada, dto.Modelo, dto.Ano);
        await _veiculoRepository.AtualizarAsync(veiculo, cancellationToken);
    }

    public async Task RemoverAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var veiculo = await _veiculoRepository.ObterPorIdAsync(id, cancellationToken: cancellationToken);
        if (veiculo == null)
            throw new RecursoNaoEncontradoException($"Veículo com Id '{id}' não foi encontrado.");

        await _veiculoRepository.RemoverAsync(veiculo, cancellationToken);
    }
}
