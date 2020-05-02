using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class UCDescricao : UserControl
    {
        public UCDescricao()
        {
            InitializeComponent();
        }

        public string Descricao
        {
            get
            {
                return TxtDescricao.Text.Trim();
            }
        }
    }
}
