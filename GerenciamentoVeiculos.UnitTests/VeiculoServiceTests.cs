using FluentValidation;
using GerenciamentoVeiculos.Application.DTOs;
using GerenciamentoVeiculos.Application.Services;
using GerenciamentoVeiculos.Application.Validators;
using GerenciamentoVeiculos.Domain.Entities;
using GerenciamentoVeiculos.Domain.Events;
using GerenciamentoVeiculos.Domain.Exceptions;
using GerenciamentoVeiculos.Domain.Interfaces.Messaging;
using GerenciamentoVeiculos.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace GerenciamentoVeiculos.UnitTests;

public class VeiculoServiceTests
{
    private readonly Mock<IVeiculoRepository> _veiculoRepositoryMock;
    private readonly Mock<IProprietarioRepository> _proprietarioRepositoryMock;
    private readonly Mock<IFilaMensageriaService> _filaMensageriaMock;
    private readonly IValidator<VeiculoCriacaoDto> _criacaoValidator;
    private readonly IValidator<VeiculoAtualizacaoDto> _atualizacaoValidator;
    private readonly VeiculoService _service;

    public VeiculoServiceTests()
    {
        _veiculoRepositoryMock = new Mock<IVeiculoRepository>();
        _proprietarioRepositoryMock = new Mock<IProprietarioRepository>();
        _filaMensageriaMock = new Mock<IFilaMensageriaService>();
        _criacaoValidator = new VeiculoCriacaoValidator();
        _atualizacaoValidator = new VeiculoAtualizacaoValidator();

        _service = new VeiculoService(
            _veiculoRepositoryMock.Object,
            _proprietarioRepositoryMock.Object,
            _filaMensageriaMock.Object,
            _criacaoValidator,
            _atualizacaoValidator);
    }

    [Fact]
    public async Task CriarAsync_QuandoProprietarioNaoExiste_DeveLancarRecursoNaoEncontradoException()
    {
        // Arrange
        var proprietarioId = Guid.NewGuid();
        var dto = new VeiculoCriacaoDto("ABC-1234", "Civic", 2022, proprietarioId);

        // Simula que o proprietário NÃO foi encontrado (regra obrigatória do teste!)
        _proprietarioRepositoryMock.Setup(r => r.ObterPorIdAsync(proprietarioId, false, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Proprietario?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<RecursoNaoEncontradoException>(() => _service.CriarAsync(dto));
        Assert.Contains("não existe no sistema", ex.Message);

        _veiculoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Veiculo>(), It.IsAny<CancellationToken>()), Times.Never);
        _filaMensageriaMock.Verify(f => f.EnviarAsync(It.IsAny<VeiculoCadastradoEvento>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_QuandoDadosSaoValidos_DeveCadastrarEEnviarMensagemParaFilaAzure()
    {
        // Arrange
        var proprietarioId = Guid.NewGuid();
        var proprietario = new Proprietario("Ayrton Senna", "12345678909", new DateTime(1960, 3, 21), proprietarioId);
        var dto = new VeiculoCriacaoDto("ABC-1234", "Civic", 2022, proprietarioId);

        _proprietarioRepositoryMock.Setup(r => r.ObterPorIdAsync(proprietarioId, false, It.IsAny<CancellationToken>()))
            .ReturnsAsync(proprietario);

        _veiculoRepositoryMock.Setup(r => r.ExistePlacaAsync(It.IsAny<string>(), null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var resultado = await _service.CriarAsync(dto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("ABC-1234", resultado.Placa);
        Assert.Equal("Civic", resultado.Modelo);
        _veiculoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Veiculo>(), It.IsAny<CancellationToken>()), Times.Once);

        // Valida que o evento foi enviado para a Fila da Azure (requisito obrigatório!)
        _filaMensageriaMock.Verify(f => f.EnviarAsync(It.Is<VeiculoCadastradoEvento>(e =>
            e.Placa == "ABC-1234" &&
            e.Modelo == "Civic" &&
            e.ProprietarioId == proprietarioId
        ), It.IsAny<CancellationToken>()), Times.Once);
    }
}
