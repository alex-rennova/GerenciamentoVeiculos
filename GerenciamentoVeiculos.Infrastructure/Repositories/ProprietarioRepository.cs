using GerenciamentoVeiculos.Domain.Entities;
using GerenciamentoVeiculos.Domain.Interfaces.Repositories;
using GerenciamentoVeiculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoVeiculos.Infrastructure.Repositories;

public class ProprietarioRepository : IProprietarioRepository
{
    private readonly AppDbContext _context;

    public ProprietarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Proprietario>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Proprietarios
            .AsNoTracking()
            .Include(p => p.Veiculos)
            .OrderBy(p => p.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task<Proprietario?> ObterPorIdAsync(Guid id, bool incluirVeiculos = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Proprietarios.AsQueryable();

        if (incluirVeiculos)
            query = query.Include(p => p.Veiculos);

        return await query.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Proprietario?> ObterPorCpfAsync(string cpf, CancellationToken cancellationToken = default)
    {
        return await _context.Proprietarios
            .FirstOrDefaultAsync(p => p.Cpf == cpf, cancellationToken);
    }

    public async Task<bool> ExisteCpfAsync(string cpf, Guid? idIgnorar = null, CancellationToken cancellationToken = default)
    {
        return await _context.Proprietarios
            .AnyAsync(p => p.Cpf == cpf && (!idIgnorar.HasValue || p.Id != idIgnorar.Value), cancellationToken);
    }

    public async Task<bool> PossuiVeiculosVinculadosAsync(Guid proprietarioId, CancellationToken cancellationToken = default)
    {
        return await _context.Veiculos
            .AnyAsync(v => v.ProprietarioId == proprietarioId, cancellationToken);
    }

    public async Task AdicionarAsync(Proprietario proprietario, CancellationToken cancellationToken = default)
    {
        await _context.Proprietarios.AddAsync(proprietario, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Proprietario proprietario, CancellationToken cancellationToken = default)
    {
        _context.Proprietarios.Update(proprietario);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Proprietario proprietario, CancellationToken cancellationToken = default)
    {
        _context.Proprietarios.Remove(proprietario);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
