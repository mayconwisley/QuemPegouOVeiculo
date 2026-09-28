using System;
using System.Collections.Generic;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;
using QuemPegouOVeiculo.Desktop.Models;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas
{
    public static class Insert { public static bool Register(MotoristaObj x) => ApiCliente.Gravar("motoristas", 0, 'I', Corpos.Motorista(x)); }
    public static class Update { public static bool Register(MotoristaObj x) => ApiCliente.Gravar("motoristas", x.Id, 'U', Corpos.Motorista(x)); }
    public static class Delete { public static bool Register(MotoristaObj x) => ApiCliente.Gravar("motoristas", x.Id, 'D'); }
}
