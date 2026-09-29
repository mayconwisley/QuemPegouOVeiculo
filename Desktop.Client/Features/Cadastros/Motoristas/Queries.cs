using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiCliente.Listar("motoristas", ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiCliente.ListarAsync("motoristas", ApiCliente.Filtros("busca", ApiCliente.Busca(search)), cancellationToken);
        public static DataTable RegisterDriverActive(string search)
        {
            var tabela = search == "%" ? ApiCliente.Listar("motoristas")
                : ApiCliente.Listar("motoristas", ApiCliente.Filtros("ativo", search == "A"));
            tabela.Columns["NumCNH"].ColumnName = "CNH";
            return tabela;
        }
        public static DataTable IdAndNameActive() => ApiCliente.Listar("motoristas", ApiCliente.Filtros("ativo", true));
        public static Task<DataTable> IdAndNameActiveAsync(CancellationToken cancellationToken) =>
            ApiCliente.ListarAsync("motoristas", ApiCliente.Filtros("ativo", true), cancellationToken);
    }
}
