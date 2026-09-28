using System;
using System.Data;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiCliente.Listar("motoristas", ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static DataTable RegisterDriverActive(string search)
        {
            var tabela = search == "%" ? ApiCliente.Listar("motoristas")
                : ApiCliente.Listar("motoristas", ApiCliente.Filtros("ativo", search == "A"));
            tabela.Columns["NumCNH"].ColumnName = "CNH";
            return tabela;
        }
        public static DataTable IdAndNameActive() => ApiCliente.Listar("motoristas", ApiCliente.Filtros("ativo", true));
    }
}
