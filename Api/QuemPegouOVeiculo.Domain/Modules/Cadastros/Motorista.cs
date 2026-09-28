using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Domain.Modules.Cadastros;

public sealed class Motorista : IEntity
{
    private Motorista() { }

    public Motorista(string nome, string cnh, DateOnly vencimentoCnh, string categoriaCnh, string cpf, string? rg, bool ativo)
    {
        Atualizar(nome, cnh, vencimentoCnh, categoriaCnh, cpf, rg, ativo);
    }

    public int Id { get; private set; }
    public string Nome { get; private set; } = "";
    public string Cnh { get; private set; } = "";
    public DateOnly VencimentoCnh { get; private set; }
    public string CategoriaCnh { get; private set; } = "";
    public string Cpf { get; private set; } = "";
    public string Rg { get; private set; } = "";
    public bool Ativo { get; private set; }

    public void Atualizar(string nome, string cnh, DateOnly vencimentoCnh, string categoriaCnh, string cpf, string? rg, bool ativo)
    {
        var nomeValidado = Guard.Required(nome, "Nome", 150);
        var cnhValidada = Guard.Required(cnh, "CNH", 20);
        var vencimentoValidado = Guard.Date(vencimentoCnh, "Vencimento da CNH");
        var categoriaValidada = Guard.Required(categoriaCnh, "Categoria da CNH", 10);
        var cpfValidado = Guard.Cpf(cpf);
        var rgValidado = Guard.Optional(rg, "RG", 20);

        Nome = nomeValidado;
        Cnh = cnhValidada;
        VencimentoCnh = vencimentoValidado;
        CategoriaCnh = categoriaValidada;
        Cpf = cpfValidado;
        Rg = rgValidado;
        Ativo = ativo;
    }
}
