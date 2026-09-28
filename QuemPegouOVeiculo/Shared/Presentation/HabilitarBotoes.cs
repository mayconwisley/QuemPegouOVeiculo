using System.Windows.Forms;

namespace QuemPegouOVeiculo.Shared.Presentation
{
    public static class HabilitarBotoes
    {
        public static void DefinirModoEdicao(bool emEdicao, Button alterar, Button excluir, Button gravar)
        {
            alterar.Enabled = emEdicao;
            excluir.Enabled = emEdicao;
            gravar.Enabled = !emEdicao;
        }
    }
}
