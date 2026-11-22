using FluentValidation;
using GerenciamentoVeiculos.Application.DTOs;
using GerenciamentoVeiculos.Application.Services;
using GerenciamentoVeiculos.Application.Validators;
using GerenciamentoVeiculos.Domain.Entities;
using GerenciamentoVeiculos.Domain.Exceptions;
using GerenciamentoVeiculos.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace GerenciamentoVeiculos.UnitTests;

public class ProprietarioServiceTests
{
    private readonly Mock<IProprietarioRepository> _repositoryMock;
    private readonly IValidator<ProprietarioCriacaoDto> _criacaoValidator;
    private readonly IValidator<ProprietarioAtualizacaoDto> _atualizacaoValidator;
    private readonly ProprietarioService _service;

    public ProprietarioServiceTests()
    {
        _repositoryMock = new Mock<IProprietarioRepository>();
        _criacaoValidator = new ProprietarioCriacaoValidator();
        _atualizacaoValidator = new ProprietarioAtualizacaoValidator();

        _service = new ProprietarioService(
            _repositoryMock.Object,
            _criacaoValidator,
            _atualizacaoValidator);
    }

    [Fact]
    public async Task CriarAsync_QuandoDadosValidos_DeveCadastrarComSucesso()
    {
        // Arrange (Preparação)
        // CPF matematicamente válido gerado para o teste
        var dto = new ProprietarioCriacaoDto("Ayrton Senna", "52998224725", new DateTime(1960, 3, 21));

        _repositoryMock.Setup(r => r.ExisteCpfAsync("52998224725", null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act (Ação)
        var resultado = await _service.CriarAsync(dto);

        // Assert (Verificação)
        Assert.NotNull(resultado);
        Assert.Equal("Ayrton Senna", resultado.Nome);
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Proprietario>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CriarAsync_QuandoCpfJaCadastrado_DeveLancarConflitoException()
    {
        // Arrange
        var dto = new ProprietarioCriacaoDto("Ayrton Senna", "52998224725", new DateTime(1960, 3, 21));

        _repositoryMock.Setup(r => r.ExisteCpfAsync("52998224725", null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true); // CPF já existe no banco

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ConflitoException>(() => _service.CriarAsync(dto));
        Assert.Contains("Já existe um proprietário cadastrado com o CPF", ex.Message);
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Proprietario>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ObterPorIdAsync_QuandoProprietarioNaoExiste_DeveLancarRecursoNaoEncontradoException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();
        _repositoryMock.Setup(r => r.ObterPorIdAsync(idInexistente, true, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Proprietario?)null);

        // Act & Assert
        await Assert.ThrowsAsync<RecursoNaoEncontradoException>(() => _service.ObterPorIdAsync(idInexistente));
    }

    [Fact]
    public async Task RemoverAsync_QuandoProprietarioPossuiVeiculos_DeveLancarConflitoException()
    {
        // Arrange
        var proprietarioId = Guid.NewGuid();
        var proprietario = new Proprietario("Ayrton Senna", "52998224725", new DateTime(1960, 3, 21), proprietarioId);

        _repositoryMock.Setup(r => r.ObterPorIdAsync(proprietarioId, false, It.IsAny<CancellationToken>()))
            .ReturnsAsync(proprietario);

        // Regra de negócio obrigatória do teste: Simula que possui veículos vinculados!
        _repositoryMock.Setup(r => r.PossuiVeiculosVinculadosAsync(proprietarioId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act & Assert
        // Tentamos remover, mas a regra bloqueia e lança ConflitoException
        var ex = await Assert.ThrowsAsync<ConflitoException>(() => _service.RemoverAsync(proprietarioId));
        Assert.Contains("Não é permitido excluir o proprietário", ex.Message);

        // Garante que o método do banco para apagar NUNCA foi chamado
        _repositoryMock.Verify(r => r.RemoverAsync(It.IsAny<Proprietario>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RemoverAsync_QuandoProprietarioNaoExiste_DeveLancarRecursoNaoEncontradoException()
    {
        // Arrange
        var proprietarioId = Guid.NewGuid();
        _repositoryMock.Setup(r => r.ObterPorIdAsync(proprietarioId, false, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Proprietario?)null);

        // Act & Assert
        await Assert.ThrowsAsync<RecursoNaoEncontradoException>(() => _service.RemoverAsync(proprietarioId));
        _repositoryMock.Verify(r => r.RemoverAsync(It.IsAny<Proprietario>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RemoverAsync_QuandoProprietarioNaoPossuiVeiculos_DeveRemoverComSucesso()
    {
        // Arrange
        var proprietarioId = Guid.NewGuid();
        var proprietario = new Proprietario("Felipe Massa", "52998224725", new DateTime(1981, 4, 25), proprietarioId);

        _repositoryMock.Setup(r => r.ObterPorIdAsync(proprietarioId, false, It.IsAny<CancellationToken>()))
            .ReturnsAsync(proprietario);

        // Proprietário NÃO tem veículos vinculados -> remoção permitida
        _repositoryMock.Setup(r => r.PossuiVeiculosVinculadosAsync(proprietarioId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        await _service.RemoverAsync(proprietarioId);

        // Assert
        // Garante que o repositório foi chamado exatamente 1 vez para apagar
        _repositoryMock.Verify(r => r.RemoverAsync(proprietario, It.IsAny<CancellationToken>()), Times.Once);
    }
}
