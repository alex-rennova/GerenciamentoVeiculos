using GerenciamentoVeiculos.Application.DTOs;
using GerenciamentoVeiculos.Domain.Entities;

namespace GerenciamentoVeiculos.Application.Mappings;

public static class VeiculoMappings
{
    public static Veiculo ToEntity(this VeiculoCriacaoDto dto)
    {
        return new Veiculo(
            placa: dto.Placa,
            modelo: dto.Modelo,
            ano: dto.Ano,
            proprietarioId: dto.ProprietarioId);
    }

    public static VeiculoRespostaDto ToDto(this Veiculo entity)
    {
        return new VeiculoRespostaDto(
            Id: entity.Id,
            Placa: entity.Placa,
            Modelo: entity.Modelo,
            Ano: entity.Ano,
            ProprietarioId: entity.ProprietarioId,
            NomeProprietario: entity.Proprietario?.Nome ?? "Não informado");
    }
}
