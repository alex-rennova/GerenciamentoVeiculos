using GerenciamentoVeiculos.Domain.Entities;

namespace GerenciamentoVeiculos.Infrastructure.Data;

/// <summary>
/// Responsável pelo Seed de dados iniciais no banco em memória para agilizar testes e demonstrações em live-code.
/// </summary>
public static class DbInitializer
{
    public static async Task InicializarDadosAsync(AppDbContext context)
    {
        if (context.Proprietarios.Any())
            return;

        // Proprietário 1: Ayrton Senna (CPF matematicamente válido)
        var prop1 = new Proprietario(
            nome: "Ayrton Senna da Silva",
            cpf: "12345678909",
            dataNascimento: new DateTime(1960, 3, 21),
            id: Guid.Parse("11111111-1111-1111-1111-111111111111"));

        // Proprietário 2: Rubens Barrichello (CPF matematicamente válido)
        var prop2 = new Proprietario(
            nome: "Rubens Barrichello",
            cpf: "98765432100",
            dataNascimento: new DateTime(1972, 5, 23),
            id: Guid.Parse("22222222-2222-2222-2222-222222222222"));

        // Proprietário 3: Felipe Massa (sem veículos para testar exclusão com sucesso)
        var prop3 = new Proprietario(
            nome: "Felipe Massa",
            cpf: "52998224725",
            dataNascimento: new DateTime(1981, 4, 25),
            id: Guid.Parse("33333333-3333-3333-3333-333333333333"));

        await context.Proprietarios.AddRangeAsync(prop1, prop2, prop3);

        // Veículos
        var veiculo1 = new Veiculo(
            placa: "SEN-1988",
            modelo: "McLaren MP4/4",
            ano: 1988,
            proprietarioId: prop1.Id,
            id: Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

        var veiculo2 = new Veiculo(
            placa: "BRA-2009",
            modelo: "Brawn BGP 001",
            ano: 2009,
            proprietarioId: prop2.Id,
            id: Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

        await context.Veiculos.AddRangeAsync(veiculo1, veiculo2);
        await context.SaveChangesAsync();
    }
}
