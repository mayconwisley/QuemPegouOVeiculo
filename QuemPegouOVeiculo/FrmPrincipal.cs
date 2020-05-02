using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        #region Variaveis
        bool abrir = true;
        #endregion

        private void SubMenuCadVeiculo_Click(object sender, EventArgs e)
        {
            FrmCadVeiculo cadVeiculo = new FrmCadVeiculo();
            cadVeiculo.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmCadVeiculo)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                cadVeiculo.Show();
            }
        }

        private void SubMenuCadMotorista_Click(object sender, EventArgs e)
        {
            FrmCadMotorista cadMotorista = new FrmCadMotorista();
            cadMotorista.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmCadMotorista)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                cadMotorista.Show();
            }
        }

        private void SubMenuCadVenciCNH_Click(object sender, EventArgs e)
        {
            FrmCadVencCNH cadVencCNH = new FrmCadVencCNH();
            cadVencCNH.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmCadVencCNH)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                cadVencCNH.Show();
            }
        }

        private void SubMenuCadStatusVeic_Click(object sender, EventArgs e)
        {
            FrmCadStatusVeic cadStatusVeic = new FrmCadStatusVeic();
            cadStatusVeic.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmCadStatusVeic)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                cadStatusVeic.Show();
            }
        }

        private void SubMenuConVeiculo_Click(object sender, EventArgs e)
        {
            FrmCadContVeiculo cadContVeiculo = new FrmCadContVeiculo();
            cadContVeiculo.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmCadContVeiculo)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                cadContVeiculo.Show();
            }
        }

        private void SubMenuConAbastecimento_Click(object sender, EventArgs e)
        {
            FrmContCombustivel contCombustivel = new FrmContCombustivel();
            contCombustivel.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmContCombustivel)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                contCombustivel.Show();
            }
        }

        private void SubMenuConMulta_Click(object sender, EventArgs e)
        {
            FrmContMulta contMulta = new FrmContMulta();
            contMulta.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmContMulta)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                contMulta.Show();
            }
        }

        private void SubMenuConManutencao_Click(object sender, EventArgs e)
        {
            FrmContManutencao contManutencao = new FrmContManutencao();
            contManutencao.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmContManutencao)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                contManutencao.Show();
            }
        }

        private void SubMenuRelConVeiculo_Click(object sender, EventArgs e)
        {
            FrmRelContVeiculo relContVeiculo = new FrmRelContVeiculo();
            relContVeiculo.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmRelContVeiculo)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                relContVeiculo.Show();
            }
        }

        private void SubMenuRelCadMotorista_Click(object sender, EventArgs e)
        {
            FrmRelMotorista relMotorista = new FrmRelMotorista();
            relMotorista.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmRelMotorista)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                relMotorista.Show();
            }
        }

        private void SubMenuRelCadVeiculo_Click(object sender, EventArgs e)
        {
            FrmRelVeiculo relVeiculo = new FrmRelVeiculo();
            relVeiculo.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmRelVeiculo)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                relVeiculo.Show();
            }
        }

        private void SubMenuRelCadStatusVeic_Click(object sender, EventArgs e)
        {
            FrmRelStatusVeiculo relStatusVeiculo = new FrmRelStatusVeiculo();
            relStatusVeiculo.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmRelStatusVeiculo)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                relStatusVeiculo.Show();
            }
        }

        private void MenuSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SubMenuRelConManutencao_Click(object sender, EventArgs e)
        {
            FrmRelContManutencao relContManutencao = new FrmRelContManutencao();
            relContManutencao.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmRelContManutencao)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                relContManutencao.Show();
            }
        }

        private void SubMenuRelConAbastecimento_Click(object sender, EventArgs e)
        {
            FrmRelContCombustivel relContCombustivel = new FrmRelContCombustivel();
            relContCombustivel.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmRelContCombustivel)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                relContCombustivel.Show();
            }
        }

        private void SubMenuRelConMulta_Click(object sender, EventArgs e)
        {
            FrmRelContMulta relContMulta = new FrmRelContMulta();
            relContMulta.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmRelContMulta)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                relContMulta.Show();
            }
        }

        private void SubMenuCheControle_Click(object sender, EventArgs e)
        {
            FrmAtuControleVeiculo atuControleVeiculo = new FrmAtuControleVeiculo();
            atuControleVeiculo.MdiParent = this;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmAtuControleVeiculo)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                atuControleVeiculo.Show();
            }
        }
    }
}
