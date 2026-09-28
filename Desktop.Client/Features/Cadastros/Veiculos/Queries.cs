using System;
using System.Data;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Veiculos
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiCliente.Listar("veiculos", ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static DataTable RegisterVehicleStaus(string search) => search == "%"
            ? ApiCliente.Listar("veiculos") : ApiCliente.Listar("veiculos", ApiCliente.Filtros("ativo", search == "A"));
        public static DataTable IdAndModelActive() => ApiCliente.Listar("veiculos", ApiCliente.Filtros("ativo", true));
        public static string UltimoKmVeiculo(int idVeiculo) => ApiCliente.UltimaQuilometragem(idVeiculo, "movimentacao");
    }
}
