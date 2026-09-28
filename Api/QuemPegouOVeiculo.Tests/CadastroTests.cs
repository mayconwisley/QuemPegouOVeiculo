using QuemPegouOVeiculo.Domain.Common;
using QuemPegouOVeiculo.Domain.Modules.Cadastros;

namespace QuemPegouOVeiculo.Tests;

public sealed class CadastroTests
{
    [Fact]
    public void Motorista_NormalizaCpf()
    {
        var motorista = new Motorista("Ana", "12345678901", new DateOnly(2028, 1, 1),
            "B", "529.982.247-25", null, true);

        Assert.Equal("52998224725", motorista.Cpf);
    }

    [Fact]
    public void Motorista_RejeitaCpfInvalido()
    {
        Assert.Throws<DomainException>(() => new Motorista("Ana", "12345678901",
            new DateOnly(2028, 1, 1), "B", "11111111111", null, true));
    }

    [Fact]
    public void Veiculo_NormalizaPlaca()
    {
        var veiculo = new Veiculo("abc-1d23", "Modelo", null, null, true);

        Assert.Equal("ABC1D23", veiculo.Placa);
    }
}
