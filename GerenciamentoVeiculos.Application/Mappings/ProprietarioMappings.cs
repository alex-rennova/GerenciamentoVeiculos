using GerenciamentoVeiculos.Application.DTOs;
using GerenciamentoVeiculos.Domain.Entities;
using GerenciamentoVeiculos.Domain.Validation;

namespace GerenciamentoVeiculos.Application.Mappings;

public static class ProprietarioMappings
{
    public static Proprietario ToEntity(this ProprietarioCriacaoDto dto)
    {
        return new Proprietario(
            nome: dto.Nome,
            cpf: dto.Cpf,
            dataNascimento: dto.DataNascimento);
    }

    public static ProprietarioRespostaDto ToDto(this Proprietario entity, bool incluirVeiculos = false)
    {
        var veiculosDto = incluirVeiculos && entity.Veiculos != null
            ? entity.Veiculos.Select(v => new VeiculoResumoDto(v.Id, v.Placa, v.Modelo, v.Ano)).ToList()
            : null;

        return new ProprietarioRespostaDto(
            Id: entity.Id,
            Nome: entity.Nome,
            Cpf: entity.Cpf,
            CpfFormatado: ValidadorCpf.Formatar(entity.Cpf),
            DataNascimento: entity.DataNascimento,
            QuantidadeVeiculos: entity.Veiculos?.Count ?? 0,
            Veiculos: veiculosDto);
    }
}
