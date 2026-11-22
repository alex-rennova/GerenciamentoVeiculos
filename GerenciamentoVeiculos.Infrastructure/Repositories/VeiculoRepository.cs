using GerenciamentoVeiculos.Domain.Entities;
using GerenciamentoVeiculos.Domain.Interfaces.Repositories;
using GerenciamentoVeiculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoVeiculos.Infrastructure.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly AppDbContext _context;

    public VeiculoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Veiculo>> ObterComFiltroAsync(
        string? placa = null,
        string? modelo = null,
        string? nomeProprietario = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Veiculos
            .AsNoTracking()
            .Include(v => v.Proprietario)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(placa))
        {
            var placaFiltro = placa.Trim().ToUpperInvariant();
            query = query.Where(v => v.Placa.ToUpper().Contains(placaFiltro));
        }

        if (!string.IsNullOrWhiteSpace(modelo))
        {
            var modeloFiltro = modelo.Trim().ToUpperInvariant();
            query = query.Where(v => v.Modelo.ToUpper().Contains(modeloFiltro));
        }

        if (!string.IsNullOrWhiteSpace(nomeProprietario))
        {
            var nomeFiltro = nomeProprietario.Trim().ToUpperInvariant();
            query = query.Where(v => v.Proprietario != null && v.Proprietario.Nome.ToUpper().Contains(nomeFiltro));
        }

        return await query
            .OrderBy(v => v.Modelo)
            .ToListAsync(cancellationToken);
    }

    public async Task<Veiculo?> ObterPorIdAsync(Guid id, bool incluirProprietario = true, CancellationToken cancellationToken = default)
    {
        var query = _context.Veiculos.AsQueryable();

        if (incluirProprietario)
            query = query.Include(v => v.Proprietario);

        return await query.FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public async Task<Veiculo?> ObterPorPlacaAsync(string placa, CancellationToken cancellationToken = default)
    {
        return await _context.Veiculos
            .Include(v => v.Proprietario)
            .FirstOrDefaultAsync(v => v.Placa == placa, cancellationToken);
    }

    public async Task<bool> ExistePlacaAsync(string placa, Guid? idIgnorar = null, CancellationToken cancellationToken = default)
    {
        return await _context.Veiculos
            .AnyAsync(v => v.Placa == placa && (!idIgnorar.HasValue || v.Id != idIgnorar.Value), cancellationToken);
    }

    public async Task AdicionarAsync(Veiculo veiculo, CancellationToken cancellationToken = default)
    {
        await _context.Veiculos.AddAsync(veiculo, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Veiculo veiculo, CancellationToken cancellationToken = default)
    {
        _context.Veiculos.Update(veiculo);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Veiculo veiculo, CancellationToken cancellationToken = default)
    {
        _context.Veiculos.Remove(veiculo);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
