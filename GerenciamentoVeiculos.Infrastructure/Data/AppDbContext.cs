using GerenciamentoVeiculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoVeiculos.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Proprietario> Proprietarios => Set<Proprietario>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeamento de Proprietário
        modelBuilder.Entity<Proprietario>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            entity.HasIndex(p => p.Cpf)
                .IsUnique();

            entity.Property(p => p.DataNascimento)
                .IsRequired();

            // Relacionamento 1:N com Veículos
            entity.HasMany(p => p.Veiculos)
                .WithOne(v => v.Proprietario)
                .HasForeignKey(v => v.ProprietarioId)
                .OnDelete(DeleteBehavior.Restrict); // Impede exclusão em cascata por segurança
        });

        // Mapeamento de Veículo
        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(v => v.Id);

            entity.Property(v => v.Placa)
                .IsRequired()
                .HasMaxLength(10);

            entity.HasIndex(v => v.Placa)
                .IsUnique();

            entity.Property(v => v.Modelo)
                .IsRequired()
                .HasMaxLength(80);

            entity.Property(v => v.Ano)
                .IsRequired();

            entity.Property(v => v.ProprietarioId)
                .IsRequired();
        });
    }
}
