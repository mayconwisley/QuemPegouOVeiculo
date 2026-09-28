using System;
using System.Collections.Generic;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;
using QuemPegouOVeiculo.Desktop.Models;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos
{
    public static class Insert { public static bool Register(StatusVeiculoObj x) => ApiCliente.Gravar("status-veiculo", 0, 'I', Corpos.StatusVeiculo(x)); }
    public static class Update { public static bool Register(StatusVeiculoObj x) => ApiCliente.Gravar("status-veiculo", x.Id, 'U', Corpos.StatusVeiculo(x)); }
    public static class Delete { public static bool Register(StatusVeiculoObj x) => ApiCliente.Gravar("status-veiculo", x.Id, 'D'); }
}
