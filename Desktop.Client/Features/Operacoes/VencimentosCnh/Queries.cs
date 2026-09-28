using System;
using System.Data;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.VencimentosCnh
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiCliente.Listar("vencimentos-cnh",
            ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
    }
}
