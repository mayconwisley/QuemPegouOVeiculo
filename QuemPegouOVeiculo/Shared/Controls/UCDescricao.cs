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
