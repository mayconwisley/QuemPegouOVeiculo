using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo.Shared.Presentation
{
    internal static class MdiForms
    {
        internal static void AbrirSubstituindo(Form origem, Form novoFormulario)
        {
            if (novoFormulario == null)
                throw new ArgumentNullException(nameof(novoFormulario));

            var principal = origem.MdiParent
                ?? throw new InvalidOperationException("A janela principal não está disponível.");

            foreach (Form aberto in principal.MdiChildren)
            {
                if (aberto.GetType() == novoFormulario.GetType())
                {
                    aberto.Close();
                    break;
                }
            }

            novoFormulario.MdiParent = principal;
            novoFormulario.Show();
        }
    }
}
