using System;
using System.Collections.Generic;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;
using QuemPegouOVeiculo.Desktop.Models;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Veiculos
{
    public static class Insert { public static bool Register(VeiculoObj x) => ApiCliente.Gravar("veiculos", 0, 'I', Corpos.Veiculo(x)); }
    public static class Update { public static bool Register(VeiculoObj x) => ApiCliente.Gravar("veiculos", x.Id, 'U', Corpos.Veiculo(x)); }
    public static class Delete { public static bool Register(VeiculoObj x) => ApiCliente.Gravar("veiculos", x.Id, 'D'); }
}
