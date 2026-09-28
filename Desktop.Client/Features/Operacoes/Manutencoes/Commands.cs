using System;
using System.Collections.Generic;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;
using QuemPegouOVeiculo.Desktop.Models;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Manutencoes
{
    public static class Insert { public static bool Register(ControleManutencaoObj x) => ApiCliente.Gravar("manutencoes", 0, 'I', Corpos.Manutencao(x)); }
    public static class Update { public static bool Register(ControleManutencaoObj x) => ApiCliente.Gravar("manutencoes", x.Id, 'U', Corpos.Manutencao(x)); }
    public static class Delete { public static bool Register(ControleManutencaoObj x) => ApiCliente.Gravar("manutencoes", x.Id, 'D'); }
}
