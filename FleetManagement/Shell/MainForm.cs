using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class MainForm : ThemedForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var mdiArea = Controls.OfType<MdiClient>().FirstOrDefault();
            if (mdiArea == null)
                return;

            mdiArea.Resize += (sender, args) => UpdateWelcomeBackground(mdiArea);
            FormClosed += (sender, args) => mdiArea.BackgroundImage?.Dispose();
            UpdateWelcomeBackground(mdiArea);
        }

        private static void UpdateWelcomeBackground(MdiClient mdiArea)
        {
            if (mdiArea.ClientSize.Width < 1 || mdiArea.ClientSize.Height < 1)
                return;

            var previous = mdiArea.BackgroundImage;
            mdiArea.BackgroundImage = CreateWelcomeBackground(mdiArea.ClientSize);
            previous?.Dispose();
        }

        private static Bitmap CreateWelcomeBackground(Size mdiArea)
        {
            var image = new Bitmap(mdiArea.Width, mdiArea.Height);
            using (var graphics = Graphics.FromImage(image))
            using (var titleFont = new Font("Segoe UI", 23F, FontStyle.Bold))
            using (var subtitleFont = new Font("Segoe UI", 11F))
            using (var accentBrush = new SolidBrush(DesktopTheme.Ink))
            using (var textBrush = new SolidBrush(DesktopTheme.Muted))
            using (var borderPen = new Pen(Color.FromArgb(215, 224, 235)))
            using (var stripeBrush = new SolidBrush(DesktopTheme.Accent))
            using (var backgroundBrush = new SolidBrush(DesktopTheme.Surface))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                graphics.Clear(DesktopTheme.Background);
                var card = new Rectangle(
                    Math.Max(0, (mdiArea.Width - 680) / 2),
                    Math.Max(0, (mdiArea.Height - 220) / 2),
                    Math.Min(680, mdiArea.Width),
                    Math.Min(220, mdiArea.Height));
                graphics.FillRectangle(backgroundBrush, card);
                graphics.DrawRectangle(borderPen, card.X, card.Y, card.Width - 1, card.Height - 1);
                graphics.FillRectangle(stripeBrush, card.X + 36, card.Y + 42, 5, 69);
                graphics.DrawString("Controle da frota", titleFont, accentBrush, card.X + 57, card.Y + 43);
                graphics.DrawString("Veículos, motoristas e movimentações em um só lugar.", subtitleFont, textBrush, card.X + 60, card.Y + 101);
                graphics.DrawString("Escolha uma opção no menu superior para começar.", subtitleFont, textBrush, card.X + 60, card.Y + 149);
            }

            return image;
        }

        private void OpenForm<T>() where T : Form, new()
        {
            foreach (Form form in MdiChildren)
            {
                if (form is T)
                {
                    form.Activate();
                    return;
                }
            }

            var newForm = new T { MdiParent = this };
            newForm.Show();
        }

        private void VehicleMenuItem_Click(object sender, EventArgs e) => OpenForm<VehicleForm>();
        private void DriverMenuItem_Click(object sender, EventArgs e) => OpenForm<DriverForm>();
        private void LicenseExpirationMenuItem_Click(object sender, EventArgs e) => OpenForm<LicenseExpirationForm>();
        private void VehicleStatusMenuItem_Click(object sender, EventArgs e) => OpenForm<VehicleStatusForm>();
        private void VehicleMovementMenuItem_Click(object sender, EventArgs e) => OpenForm<VehicleMovementForm>();
        private void RefuelingMenuItem_Click(object sender, EventArgs e) => OpenForm<RefuelingForm>();
        private void FineMenuItem_Click(object sender, EventArgs e) => OpenForm<FineForm>();
        private void MaintenanceMenuItem_Click(object sender, EventArgs e) => OpenForm<MaintenanceForm>();
        private void VehicleArrivalMenuItem_Click(object sender, EventArgs e) => OpenForm<VehicleArrivalForm>();
        private void VehicleMovementReportMenuItem_Click(object sender, EventArgs e) => OpenForm<VehicleMovementReportForm>();
        private void RefuelingReportMenuItem_Click(object sender, EventArgs e) => OpenForm<RefuelingReportForm>();
        private void MaintenanceReportMenuItem_Click(object sender, EventArgs e) => OpenForm<MaintenanceReportForm>();
        private void FineReportMenuItem_Click(object sender, EventArgs e) => OpenForm<FineReportForm>();
        private void VehicleReportMenuItem_Click(object sender, EventArgs e) => OpenForm<VehicleReportForm>();
        private void DriverReportMenuItem_Click(object sender, EventArgs e) => OpenForm<DriverReportForm>();
        private void VehicleStatusReportMenuItem_Click(object sender, EventArgs e) => OpenForm<VehicleStatusReportForm>();
        private void ExitMenuItem_Click(object sender, EventArgs e) => Close();
    }
}
