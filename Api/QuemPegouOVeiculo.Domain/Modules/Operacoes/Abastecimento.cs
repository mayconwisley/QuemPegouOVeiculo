using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Domain.Modules.Operacoes;

public sealed class Abastecimento : IEntity
{
    private Abastecimento() { }

    public Abastecimento(int veiculoId, int motoristaId, int quilometragem, DateOnly data, decimal valor, decimal litros, string? descricao)
    {
        Atualizar(veiculoId, motoristaId, quilometragem, data, valor, litros, descricao);
    }

    public int Id { get; private set; }
    public int VeiculoId { get; private set; }
    public int MotoristaId { get; private set; }
    public int Quilometragem { get; private set; }
    public DateOnly Data { get; private set; }
    public decimal Valor { get; private set; }
    public decimal Litros { get; private set; }
    public string Descricao { get; private set; } = "";

    public void Atualizar(int veiculoId, int motoristaId, int quilometragem, DateOnly data, decimal valor, decimal litros, string? descricao)
    {
        var veiculoValidado = Guard.PositiveId(veiculoId, "Veículo");
        var motoristaValidado = Guard.PositiveId(motoristaId, "Motorista");
        var quilometragemValidada = Guard.NonNegative(quilometragem, "Quilometragem");
        var dataValidada = Guard.Date(data, "Data");
        var valorValidado = Guard.NonNegative(valor, "Valor");
        if (litros <= 0)
            throw new DomainException("Litros deve ser maior que zero.");
        var descricaoValidada = Guard.Optional(descricao, "Descrição", 2000);

        VeiculoId = veiculoValidado;
        MotoristaId = motoristaValidado;
        Quilometragem = quilometragemValidada;
        Data = dataValidada;
        Valor = valorValidado;
        Litros = litros;
        Descricao = descricaoValidada;
    }
}
