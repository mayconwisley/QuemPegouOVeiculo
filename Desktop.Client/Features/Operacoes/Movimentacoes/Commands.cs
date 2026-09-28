using System;
using System.Collections.Generic;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;
using QuemPegouOVeiculo.Desktop.Models;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes
{
    public static class Insert { public static bool Register(ControleVeiculoObj x) => ApiCliente.Gravar("movimentacoes", 0, 'I', Corpos.Movimentacao(x)); }
    public static class Update
    {
        public static bool Register(ControleVeiculoObj x) => ApiCliente.Gravar("movimentacoes", x.Id, 'U', Corpos.Movimentacao(x));
        public static bool RegisterControl(ControleVeiculoObj x) => ApiCliente.ConcluirMovimentacao(x.Id, x.DataHoraChegada, x.KmFinal);
    }
    public static class Delete { public static bool Register(ControleVeiculoObj x) => ApiCliente.Gravar("movimentacoes", x.Id, 'D'); }
}
