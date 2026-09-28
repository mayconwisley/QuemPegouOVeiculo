using QuemPegouOVeiculo.Domain.Common;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Tests;

public sealed class MovimentacaoVeiculoTests
{
    private static readonly DateTime Saida = new(2026, 9, 27, 12, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void Concluir_ExigeChegadaEQuilometragemCoerentes()
    {
        var movimentacao = new MovimentacaoVeiculo(1, 2, Saida, 100, null);

        Assert.Throws<DomainException>(() => movimentacao.Concluir(Saida.AddMinutes(-1), 110));
        Assert.Throws<DomainException>(() => movimentacao.Concluir(Saida.AddMinutes(1), 99));
    }

    [Fact]
    public void Concluir_ImpedeSegundaConclusao()
    {
        var movimentacao = new MovimentacaoVeiculo(1, 2, Saida, 100, null);

        movimentacao.Concluir(Saida.AddHours(1), 120);

        Assert.False(movimentacao.EmAberto);
        Assert.Throws<DomainException>(() => movimentacao.Concluir(Saida.AddHours(2), 130));
    }

    [Fact]
    public void Atualizar_ExigeChegadaEQuilometragemFinalJuntas()
    {
        var movimentacao = new MovimentacaoVeiculo(1, 2, Saida, 100, null);

        Assert.Throws<DomainException>(() => movimentacao.Atualizar(1, 2, Saida, Saida.AddHours(1), 100, null, null));
        Assert.Throws<DomainException>(() => movimentacao.Atualizar(1, 2, Saida, null, 100, 120, null));
        Assert.True(movimentacao.EmAberto);
        Assert.Null(movimentacao.KmFinal);
    }

    [Fact]
    public void Criar_RejeitaHorarioSemUtc()
    {
        var horarioLocal = DateTime.SpecifyKind(Saida, DateTimeKind.Local);

        Assert.Throws<DomainException>(() => new MovimentacaoVeiculo(1, 2, horarioLocal, 100, null));
    }
}
