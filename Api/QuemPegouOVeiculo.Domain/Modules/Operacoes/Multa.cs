using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Domain.Modules.Operacoes;

public sealed class Multa : IEntity
{
    private Multa() { }

    public Multa(int veiculoId, int motoristaId, DateOnly data, decimal valor, int pontos, string? descricao)
    {
        Atualizar(veiculoId, motoristaId, data, valor, pontos, descricao);
    }

    public int Id { get; private set; }
    public int VeiculoId { get; private set; }
    public int MotoristaId { get; private set; }
    public DateOnly Data { get; private set; }
    public decimal Valor { get; private set; }
    public int Pontos { get; private set; }
    public string Descricao { get; private set; } = "";

    public void Atualizar(int veiculoId, int motoristaId, DateOnly data, decimal valor, int pontos, string? descricao)
    {
        var veiculoValidado = Guard.PositiveId(veiculoId, "Veículo");
        var motoristaValidado = Guard.PositiveId(motoristaId, "Motorista");
        var dataValidada = Guard.Date(data, "Data");
        var valorValidado = Guard.NonNegative(valor, "Valor");
        var pontosValidados = Guard.NonNegative(pontos, "Pontos");
        var descricaoValidada = Guard.Optional(descricao, "Descrição", 2000);

        VeiculoId = veiculoValidado;
        MotoristaId = motoristaValidado;
        Data = dataValidada;
        Valor = valorValidado;
        Pontos = pontosValidados;
        Descricao = descricaoValidada;
    }
}
