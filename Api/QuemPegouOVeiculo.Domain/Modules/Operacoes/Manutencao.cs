using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Domain.Modules.Operacoes;

public sealed class Manutencao : IEntity
{
    private Manutencao() { }

    public Manutencao(int veiculoId, DateOnly data, decimal valor, string? descricao)
    {
        Atualizar(veiculoId, data, valor, descricao);
    }

    public int Id { get; private set; }
    public int VeiculoId { get; private set; }
    public DateOnly Data { get; private set; }
    public decimal Valor { get; private set; }
    public string Descricao { get; private set; } = "";

    public void Atualizar(int veiculoId, DateOnly data, decimal valor, string? descricao)
    {
        var veiculoValidado = Guard.PositiveId(veiculoId, "Veículo");
        var dataValidada = Guard.Date(data, "Data");
        var valorValidado = Guard.NonNegative(valor, "Valor");
        var descricaoValidada = Guard.Optional(descricao, "Descrição", 2000);

        VeiculoId = veiculoValidado;
        Data = dataValidada;
        Valor = valorValidado;
        Descricao = descricaoValidada;
    }
}
