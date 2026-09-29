using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmPrincipal : ThemedForm
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var area = Controls.OfType<MdiClient>().FirstOrDefault();
            if (area == null)
                return;

            area.Resize += (sender, args) => AtualizarBoasVindas(area);
            FormClosed += (sender, args) => area.BackgroundImage?.Dispose();
            AtualizarBoasVindas(area);
        }

        private static void AtualizarBoasVindas(MdiClient area)
        {
            if (area.ClientSize.Width < 1 || area.ClientSize.Height < 1)
                return;

            var anterior = area.BackgroundImage;
            area.BackgroundImage = CriarBoasVindas(area.ClientSize);
            anterior?.Dispose();
        }

        private static Bitmap CriarBoasVindas(Size area)
        {
            var imagem = new Bitmap(area.Width, area.Height);
            using (var grafico = Graphics.FromImage(imagem))
            using (var titulo = new Font("Segoe UI", 23F, FontStyle.Bold))
            using (var subtitulo = new Font("Segoe UI", 11F))
            using (var destaque = new SolidBrush(DesktopTheme.Ink))
            using (var texto = new SolidBrush(DesktopTheme.Muted))
            using (var borda = new Pen(Color.FromArgb(215, 224, 235)))
            using (var faixa = new SolidBrush(DesktopTheme.Accent))
            using (var fundo = new SolidBrush(DesktopTheme.Surface))
            {
                grafico.SmoothingMode = SmoothingMode.AntiAlias;
                grafico.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                grafico.Clear(DesktopTheme.Background);
                var cartao = new Rectangle(
                    Math.Max(0, (area.Width - 680) / 2),
                    Math.Max(0, (area.Height - 220) / 2),
                    Math.Min(680, area.Width),
                    Math.Min(220, area.Height));
                grafico.FillRectangle(fundo, cartao);
                grafico.DrawRectangle(borda, cartao.X, cartao.Y, cartao.Width - 1, cartao.Height - 1);
                grafico.FillRectangle(faixa, cartao.X + 36, cartao.Y + 42, 5, 69);
                grafico.DrawString("Controle da frota", titulo, destaque, cartao.X + 57, cartao.Y + 43);
                grafico.DrawString("Veículos, motoristas e movimentações em um só lugar.", subtitulo, texto, cartao.X + 60, cartao.Y + 101);
                grafico.DrawString("Escolha uma opção no menu superior para começar.", subtitulo, texto, cartao.X + 60, cartao.Y + 149);
            }

            return imagem;
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
