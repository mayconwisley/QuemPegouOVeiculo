using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void AbrirFormulario<T>() where T : Form, new()
        {
            foreach (Form formulario in MdiChildren)
            {
                if (formulario is T)
                {
                    formulario.Activate();
                    return;
                }
            }

            var novoFormulario = new T { MdiParent = this };
            novoFormulario.Show();
        }

        private void SubMenuCadVeiculo_Click(object sender, EventArgs e) => AbrirFormulario<FrmCadVeiculo>();
        private void SubMenuCadMotorista_Click(object sender, EventArgs e) => AbrirFormulario<FrmCadMotorista>();
        private void SubMenuCadVenciCNH_Click(object sender, EventArgs e) => AbrirFormulario<FrmCadVencCNH>();
        private void SubMenuCadStatusVeic_Click(object sender, EventArgs e) => AbrirFormulario<FrmCadStatusVeic>();
        private void SubMenuConVeiculo_Click(object sender, EventArgs e) => AbrirFormulario<FrmCadContVeiculo>();
        private void SubMenuConAbastecimento_Click(object sender, EventArgs e) => AbrirFormulario<FrmContCombustivel>();
        private void SubMenuConMulta_Click(object sender, EventArgs e) => AbrirFormulario<FrmContMulta>();
        private void SubMenuConManutencao_Click(object sender, EventArgs e) => AbrirFormulario<FrmContManutencao>();
        private void SubMenuCheControle_Click(object sender, EventArgs e) => AbrirFormulario<FrmAtuControleVeiculo>();
        private void SubMenuRelConVeiculo_Click(object sender, EventArgs e) => AbrirFormulario<FrmRelContVeiculo>();
        private void SubMenuRelConAbastecimento_Click(object sender, EventArgs e) => AbrirFormulario<FrmRelContCombustivel>();
        private void SubMenuRelConManutencao_Click(object sender, EventArgs e) => AbrirFormulario<FrmRelContManutencao>();
        private void SubMenuRelConMulta_Click(object sender, EventArgs e) => AbrirFormulario<FrmRelContMulta>();
        private void SubMenuRelCadVeiculo_Click(object sender, EventArgs e) => AbrirFormulario<FrmRelVeiculo>();
        private void SubMenuRelCadMotorista_Click(object sender, EventArgs e) => AbrirFormulario<FrmRelMotorista>();
        private void SubMenuRelCadStatusVeic_Click(object sender, EventArgs e) => AbrirFormulario<FrmRelStatusVeiculo>();
        private void MenuSair_Click(object sender, EventArgs e) => Close();
    }
}
