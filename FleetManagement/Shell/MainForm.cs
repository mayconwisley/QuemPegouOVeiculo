using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement
{
    public partial class MainForm : ThemedForm
    {
        private readonly ToolStripMenuItem _dashboardMenu = new ToolStripMenuItem("Painel");
        private readonly ToolStripMenuItem _administrationMenu = new ToolStripMenuItem("Administração");
        private readonly ToolStripMenuItem _planningMenu = new ToolStripMenuItem("Planejamento");
        private bool _reloginInProgress;

        public MainForm()
        {
            InitializeComponent();
            NavigationMenuStrip.Items.Insert(0, _dashboardMenu);
            _dashboardMenu.Click += (sender, args) => OpenForm<DashboardForm>();
            _administrationMenu.DropDownItems.Add("Usuários", null, (sender, args) => OpenForm<UserManagementForm>());
            _administrationMenu.DropDownItems.Add("Auditoria", null, (sender, args) => OpenForm<AuditForm>());
            NavigationMenuStrip.Items.Insert(NavigationMenuStrip.Items.Count - 1, _administrationMenu);
            _planningMenu.DropDownItems.Add("Previsões e checklists", null,
                (sender, args) => OpenForm<MovementPlanningForm>());
            _planningMenu.DropDownItems.Add("Manutenção preventiva", null,
                (sender, args) => OpenForm<MaintenancePlanningForm>());
            _planningMenu.DropDownItems.Add("Reservas", null,
                (sender, args) => OpenForm<ReservationForm>());
            NavigationMenuStrip.Items.Insert(NavigationMenuStrip.Items.Count - 1, _planningMenu);
            MenuReport.DropDownItems.Add(new ToolStripSeparator());
            MenuReport.DropDownItems.Add("Exportar CSV", null,
                (sender, args) => OpenForm<ExportForm>());
            AccessClient.SessionExpired += HandleSessionExpired;
            FormClosed += (sender, args) => AccessClient.SessionExpired -= HandleSessionExpired;
            ApplyPermissions();
        }

        private void ApplyPermissions()
        {
            var session = AccessClient.Current;
            if (session != null)
            {
                _administrationMenu.Visible = session.IsAdministrator;
                _planningMenu.Visible = true;
                MenuRegistration.Visible = session.CanWrite;
                OperationsMenuItem.Visible = session.CanWrite;
                MenuArrival.Visible = session.CanWrite;
                toolStripStatusLabel1.Text = "Usuário: " + session.Username + " · " +
                    (session.IsAdministrator ? "Administrador" : session.CanWrite ? "Operador" : "Consulta");
            }
        }

        private void HandleSessionExpired()
        {
            if (IsDisposed || !IsHandleCreated)
                return;
            BeginInvoke((Action)(() =>
            {
                if (_reloginInProgress || IsDisposed)
                    return;
                _reloginInProgress = true;
                try
                {
                    foreach (Form child in MdiChildren)
                        child.Close();
                    using (var login = new LoginForm())
                    {
                        if (login.ShowDialog(this) != DialogResult.OK)
                        {
                            Close();
                            return;
                        }
                    }
                    ApplyPermissions();
                    OpenForm<DashboardForm>();
                }
                finally { _reloginInProgress = false; }
            }));
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (AccessClient.Current != null)
                OpenForm<DashboardForm>();
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
            if (newForm is DashboardForm dashboard)
                dashboard.OpenArea += area =>
                {
                    if (area == "overdue-return" || area == "departure-checklist" ||
                        area == "arrival-checklist") OpenForm<MovementPlanningForm>();
                    if (area == "maintenance-plan") OpenForm<MaintenancePlanningForm>();
                    if (AccessClient.Current?.CanWrite != true)
                        return;
                    if (area == "license") OpenForm<DriverForm>();
                    if (area == "open-movement") OpenForm<VehicleMovementForm>();
                };
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
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            AccessClient.Logout();
            Close();
        }
    }
}
