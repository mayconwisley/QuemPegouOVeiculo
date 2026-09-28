using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Domain.Modules.Operacoes;

public sealed class MovimentacaoVeiculo : IEntity
{
    private MovimentacaoVeiculo() { }

    public MovimentacaoVeiculo(int veiculoId, int motoristaId, DateTime saidaUtc, int kmInicial, string? descricao)
    {
        Atualizar(veiculoId, motoristaId, saidaUtc, null, kmInicial, null, descricao);
    }

    public int Id { get; private set; }
    public int VeiculoId { get; private set; }
    public int MotoristaId { get; private set; }
    public DateTime SaidaUtc { get; private set; }
    public DateTime? ChegadaUtc { get; private set; }
    public int KmInicial { get; private set; }
    public int? KmFinal { get; private set; }
    public string Descricao { get; private set; } = "";
    public bool EmAberto => ChegadaUtc is null;

    public void Concluir(DateTime chegadaUtc, int kmFinal)
    {
        if (!EmAberto)
            throw new DomainException("A movimentação já foi concluída.");
        Atualizar(VeiculoId, MotoristaId, SaidaUtc, chegadaUtc, KmInicial, kmFinal, Descricao);
    }

    public void Atualizar(int veiculoId, int motoristaId, DateTime saidaUtc, DateTime? chegadaUtc,
        int kmInicial, int? kmFinal, string? descricao)
    {
        var veiculoValidado = Guard.PositiveId(veiculoId, "Veículo");
        var motoristaValidado = Guard.PositiveId(motoristaId, "Motorista");
        var saidaValidada = Guard.Utc(saidaUtc, "Saída");
        DateTime? chegadaValidada = chegadaUtc is null ? null : Guard.Utc(chegadaUtc.Value, "Chegada");
        var kmInicialValidado = Guard.NonNegative(kmInicial, "Quilometragem inicial");
        int? kmFinalValidado = kmFinal is null ? null : Guard.NonNegative(kmFinal.Value, "Quilometragem final");
        var descricaoValidada = Guard.Optional(descricao, "Descrição", 2000);

        if (chegadaValidada.HasValue != kmFinalValidado.HasValue)
            throw new DomainException("Chegada e quilometragem final devem ser informadas juntas.");
        if (chegadaValidada < saidaValidada)
            throw new DomainException("Chegada deve ser posterior à saída.");
        if (kmFinalValidado < kmInicialValidado)
            throw new DomainException("Quilometragem final deve ser maior ou igual à inicial.");

        VeiculoId = veiculoValidado;
        MotoristaId = motoristaValidado;
        SaidaUtc = saidaValidada;
        ChegadaUtc = chegadaValidada;
        KmInicial = kmInicialValidado;
        KmFinal = kmFinalValidado;
        Descricao = descricaoValidada;
    }
}
