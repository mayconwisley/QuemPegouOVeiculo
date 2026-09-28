using System;
using System.Collections.Generic;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;
using QuemPegouOVeiculo.Desktop.Models;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas
{
    public static class Insert { public static bool Register(ControleMultaObj x) => ApiCliente.Gravar("multas", 0, 'I', Corpos.Multa(x)); }
    public static class Update { public static bool Register(ControleMultaObj x) => ApiCliente.Gravar("multas", x.Id, 'U', Corpos.Multa(x)); }
    public static class Delete { public static bool Register(ControleMultaObj x) => ApiCliente.Gravar("multas", x.Id, 'D'); }
}
