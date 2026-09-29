using System;
using System.Windows.Forms;

using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class AmountControl : UserControl
    {
        public AmountControl()
        {
            InitializeComponent();
        }

        decimal amount = 0;
        public Decimal Amount
        {
            get
            {
                if (Decimal.TryParse(TxtAmount.Text.Trim(), out amount))
                {
                    return amount;
                }
                else
                {
                    return 0;
                }
            }
        }

        private void TxtAmount_TextChanged(object sender, EventArgs e)
        {
            TxtAmount.Text = AmountFormatter.Amount(TxtAmount.Text.Trim());
            TxtAmount.Select(TxtAmount.Text.Length, 0);
        }

        private void TxtAmount_Leave(object sender, EventArgs e)
        {
            TxtAmount.Text = AmountFormatter.Zero(TxtAmount.Text.Trim());
            TxtAmount.Text = AmountFormatter.ParaAmount(TxtAmount.Text.Trim());
        }

        private void TxtAmount_Enter(object sender, EventArgs e)
        {
            if (TxtAmount.Text == "0,00")
            {
                TxtAmount.Clear();
            }
        }
    }
}
