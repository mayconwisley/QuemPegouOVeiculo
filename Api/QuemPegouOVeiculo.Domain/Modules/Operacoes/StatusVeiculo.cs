using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Domain.Modules.Operacoes;

public sealed class StatusVeiculo : IEntity
{
    private StatusVeiculo() { }

    public StatusVeiculo(int veiculoId, DateTime inicioUtc, DateTime? fimUtc, string descricao)
    {
        Atualizar(veiculoId, inicioUtc, fimUtc, descricao);
    }

    public int Id { get; private set; }
    public int VeiculoId { get; private set; }
    public DateTime InicioUtc { get; private set; }
    public DateTime? FimUtc { get; private set; }
    public string Descricao { get; private set; } = "";

    public void Atualizar(int veiculoId, DateTime inicioUtc, DateTime? fimUtc, string descricao)
    {
        var veiculoValidado = Guard.PositiveId(veiculoId, "Veículo");
        var inicioValidado = Guard.Utc(inicioUtc, "Início");
        DateTime? fimValidado = fimUtc is null ? null : Guard.Utc(fimUtc.Value, "Fim");
        var descricaoValidada = Guard.Required(descricao, "Descrição", 2000);
        if (fimValidado < inicioValidado)
            throw new DomainException("Fim deve ser posterior ao início.");

        VeiculoId = veiculoValidado;
        InicioUtc = inicioValidado;
        FimUtc = fimValidado;
        Descricao = descricaoValidada;
    }
}
