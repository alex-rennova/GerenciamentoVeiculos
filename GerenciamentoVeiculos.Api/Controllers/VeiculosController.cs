using GerenciamentoVeiculos.Application.DTOs;
using GerenciamentoVeiculos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoVeiculos.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class VeiculosController : ControllerBase
{
    private readonly IVeiculoService _veiculoService;

    public VeiculosController(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    /// <summary>
    /// Consulta veículos com suporte a filtros opcionais por Placa, Modelo e Nome do Proprietário.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VeiculoRespostaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterComFiltro([FromQuery] VeiculoFiltroDto filtro, CancellationToken cancellationToken)
    {
        var veiculos = await _veiculoService.ObterComFiltroAsync(filtro, cancellationToken);
        return Ok(veiculos);
    }

    /// <summary>
    /// Obtém detalhes de um veículo específico por Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(VeiculoRespostaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var veiculo = await _veiculoService.ObterPorIdAsync(id, cancellationToken);
        return Ok(veiculo);
    }

    /// <summary>
    /// Cadastra um novo veículo vinculado a um proprietário existente e despacha evento para a Fila da Azure.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(VeiculoRespostaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Criar([FromBody] VeiculoCriacaoDto dto, CancellationToken cancellationToken)
    {
        var novoVeiculo = await _veiculoService.CriarAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id = novoVeiculo.Id }, novoVeiculo);
    }

    /// <summary>
    /// Atualiza as informações de um veículo existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] VeiculoAtualizacaoDto dto, CancellationToken cancellationToken)
    {
        await _veiculoService.AtualizarAsync(id, dto, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Remove um veículo do sistema por Id.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(Guid id, CancellationToken cancellationToken)
    {
        await _veiculoService.RemoverAsync(id, cancellationToken);
        return NoContent();
    }
}
