using System;
using System.Collections.Generic;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;
using QuemPegouOVeiculo.Desktop.Models;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos
{
    public static class Insert { public static bool Register(ControleAbastecimentoObj x) => ApiCliente.Gravar("abastecimentos", 0, 'I', Corpos.Abastecimento(x)); }
    public static class Update { public static bool Register(ControleAbastecimentoObj x) => ApiCliente.Gravar("abastecimentos", x.Id, 'U', Corpos.Abastecimento(x)); }
    public static class Delete { public static bool Register(ControleAbastecimentoObj x) => ApiCliente.Gravar("abastecimentos", x.Id, 'D'); }
}
