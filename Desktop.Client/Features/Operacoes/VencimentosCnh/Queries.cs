using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.VencimentosCnh
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiCliente.Listar("vencimentos-cnh",
            ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiCliente.ListarAsync("vencimentos-cnh", ApiCliente.Filtros("busca", ApiCliente.Busca(search)),
                cancellationToken);
    }
}
