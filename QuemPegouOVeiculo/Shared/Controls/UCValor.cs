using System;
using System.Windows.Forms;

using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class UCValor : UserControl
    {
        public UCValor()
        {
            InitializeComponent();
        }

        decimal valor = 0;
        public Decimal Valor
        {
            get
            {
                if (Decimal.TryParse(TxtValor.Text.Trim(), out valor))
                {
                    return valor;
                }
                else
                {
                    return 0;
                }
            }
        }

        private void TxtValor_TextChanged(object sender, EventArgs e)
        {
            TxtValor.Text = FormatarValor.Valor(TxtValor.Text.Trim());
            TxtValor.Select(TxtValor.Text.Length, 0);
        }

        private void TxtValor_Leave(object sender, EventArgs e)
        {
            TxtValor.Text = FormatarValor.Zero(TxtValor.Text.Trim());
            TxtValor.Text = FormatarValor.ParaValor(TxtValor.Text.Trim());
        }

        private void TxtValor_Enter(object sender, EventArgs e)
        {
            if (TxtValor.Text == "0,00")
            {
                TxtValor.Clear();
            }
        }
    }
}
