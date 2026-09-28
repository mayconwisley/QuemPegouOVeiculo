using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Domain.Modules.Operacoes;

public sealed class VencimentoCnh : IEntity
{
    private VencimentoCnh() { }

    public VencimentoCnh(int motoristaId, DateOnly data, bool vencido)
    {
        Atualizar(motoristaId, data, vencido);
    }

    public int Id { get; private set; }
    public int MotoristaId { get; private set; }
    public DateOnly Data { get; private set; }
    public bool Vencido { get; private set; }

    public void Atualizar(int motoristaId, DateOnly data, bool vencido)
    {
        var motoristaValidado = Guard.PositiveId(motoristaId, "Motorista");
        var dataValidada = Guard.Date(data, "Data");

        MotoristaId = motoristaValidado;
        Data = dataValidada;
        Vencido = vencido;
    }
}
