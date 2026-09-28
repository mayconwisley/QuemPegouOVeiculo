using System;
using System.Collections.Generic;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;
using QuemPegouOVeiculo.Desktop.Models;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.VencimentosCnh
{
    public static class Insert { public static bool Register(VencimentoCNHObj x) => ApiCliente.Gravar("vencimentos-cnh", 0, 'I', Corpos.VencimentoCnh(x)); }
    public static class Update { public static bool Register(VencimentoCNHObj x) => ApiCliente.Gravar("vencimentos-cnh", x.Id, 'U', Corpos.VencimentoCnh(x)); }
    public static class Delete { public static bool Register(VencimentoCNHObj x) => ApiCliente.Gravar("vencimentos-cnh", x.Id, 'D'); }
}
