using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Domain.Modules.Cadastros;

public sealed class Veiculo : IEntity
{
    private Veiculo() { }

    public Veiculo(string placa, string modelo, string? chassi, string? renavam, bool ativo)
    {
        Atualizar(placa, modelo, chassi, renavam, ativo);
    }

    public int Id { get; private set; }
    public string Placa { get; private set; } = "";
    public string Modelo { get; private set; } = "";
    public string Chassi { get; private set; } = "";
    public string Renavam { get; private set; } = "";
    public bool Ativo { get; private set; }

    public void Atualizar(string placa, string modelo, string? chassi, string? renavam, bool ativo)
    {
        var placaValidada = Guard.Plate(placa);
        var modeloValidado = Guard.Required(modelo, "Modelo", 150);
        var chassiValidado = Guard.Optional(chassi, "Chassi", 30);
        var renavamValidado = Guard.Optional(renavam, "Renavam", 20);

        Placa = placaValidada;
        Modelo = modeloValidado;
        Chassi = chassiValidado;
        Renavam = renavamValidado;
        Ativo = ativo;
    }
}
