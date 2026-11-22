using GerenciamentoVeiculos.Application.DTOs;
using GerenciamentoVeiculos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoVeiculos.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ProprietariosController : ControllerBase
{
    private readonly IProprietarioService _proprietarioService;

    public ProprietariosController(IProprietarioService proprietarioService)
    {
        _proprietarioService = proprietarioService;
    }

    /// <summary>
    /// Lista todos os proprietários cadastrados com contagem e detalhes de seus veículos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProprietarioRespostaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos(CancellationToken cancellationToken)
    {
        var proprietarios = await _proprietarioService.ObterTodosAsync(cancellationToken);
        return Ok(proprietarios);
    }

    /// <summary>
    /// Obtém os dados de um proprietário específico por Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProprietarioRespostaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var proprietario = await _proprietarioService.ObterPorIdAsync(id, cancellationToken);
        return Ok(proprietario);
    }

    /// <summary>
    /// Cadastra um novo proprietário no sistema.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ProprietarioRespostaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Criar([FromBody] ProprietarioCriacaoDto dto, CancellationToken cancellationToken)
    {
        var novoProprietario = await _proprietarioService.CriarAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id = novoProprietario.Id }, novoProprietario);
    }

    /// <summary>
    /// Atualiza os dados de um proprietário existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] ProprietarioAtualizacaoDto dto, CancellationToken cancellationToken)
    {
        await _proprietarioService.AtualizarAsync(id, dto, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Exclui um proprietário caso ele não possua veículos vinculados.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Remover(Guid id, CancellationToken cancellationToken)
    {
        await _proprietarioService.RemoverAsync(id, cancellationToken);
        return NoContent();
    }
}
