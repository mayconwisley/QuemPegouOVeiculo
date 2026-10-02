using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement
{
    public sealed class DashboardForm : Form
    {
        private readonly FlowLayoutPanel _cards = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 210,
            Padding = new Padding(18, 12, 0, 0), AutoScroll = true };
        private readonly DataGridView _attention = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true,
            AutoGenerateColumns = false, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
            RowHeadersVisible = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
        private readonly Label _state = new Label { AutoSize = true, Text = "Carregando..." };
        private readonly Button _refresh = new Button { Text = "Atualizar", Width = 100 };
        private DashboardOverview _overview;

        public event Action<string> OpenArea;

        public DashboardForm()
        {
            Text = "Painel operacional";
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.FromArgb(244, 247, 251);
            MinimumSize = new Size(800, 480);

            var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 47, Padding = new Padding(18, 9, 0, 0) };
            toolbar.Controls.Add(_refresh);
            toolbar.Controls.Add(_state);
            _refresh.Click += async (sender, args) => await LoadDashboardAsync();

            _attention.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Atenção", DataPropertyName = "Kind",
                Width = 150 });
            _attention.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Registro", DataPropertyName = "Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            _attention.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Data", DataPropertyName = "Date",
                Width = 180 });
            _attention.CellDoubleClick += (sender, args) =>
            {
                if (args.RowIndex >= 0 && _overview != null && args.RowIndex < _overview.Attention.Count)
                    OpenArea?.Invoke(_overview.Attention[args.RowIndex].Kind);
            };
            var heading = new Label { Text = "Pendências e registros que exigem atenção",
                Dock = DockStyle.Top, Height = 38, Padding = new Padding(20, 10, 0, 0),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold) };
            Controls.Add(_attention);
            Controls.Add(heading);
            Controls.Add(_cards);
            Controls.Add(toolbar);
            Shown += async (sender, args) => await LoadDashboardAsync();
        }

        private async Task LoadDashboardAsync()
        {
            _refresh.Enabled = false;
            _state.Text = "Atualizando painel...";
            try
            {
                _overview = await AccessClient.GetDashboardAsync();
                _cards.Controls.Clear();
                AddCard("Veículos ativos", _overview.ActiveVehicles);
                AddCard("Motoristas ativos", _overview.ActiveDrivers);
                AddCard("Movimentações abertas", _overview.OpenMovements);
                AddCard("Status sem fim", _overview.OpenVehicleStatuses);
                AddCard("CNHs até 30 dias", _overview.ExpiringLicenses);
                AddCard("Retornos atrasados", _overview.OverdueReturns);
                AddCard("Checklists pendentes", _overview.PendingChecklists);
                AddCard("Revisões até 30 dias", _overview.DueMaintenancePlans);
                _attention.DataSource = _overview.Attention.Select(x => new
                {
                    Kind = AttentionLabel(x.Kind),
                    x.Description,
                    Date = x.DueDate?.ToString("dd/MM/yyyy") ?? x.OccurredAtUtc?.ToLocalTime().ToString("dd/MM/yyyy HH:mm")
                }).ToList();
                _state.Text = "Atualizado em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception exception) { _state.Text = exception.Message; }
            finally { _refresh.Enabled = true; }
        }

        private static string AttentionLabel(string kind)
        {
            switch (kind)
            {
                case "license": return "CNH";
                case "overdue-return": return "Retorno atrasado";
                case "departure-checklist": return "Checklist de saída";
                case "arrival-checklist": return "Checklist de chegada";
                case "maintenance-plan": return "Manutenção preventiva";
                default: return "Em uso há mais de 24 h";
            }
        }

        private void AddCard(string title, int count)
        {
            var panel = new Panel { Width = 170, Height = 82, BackColor = Color.White, Margin = new Padding(0, 0, 12, 0) };
            panel.Controls.Add(new Label { Text = title, AutoSize = true, Location = new Point(12, 10),
                ForeColor = Color.FromArgb(98, 111, 130) });
            panel.Controls.Add(new Label { Text = count.ToString(), AutoSize = true, Location = new Point(12, 34),
                Font = new Font("Segoe UI", 20F, FontStyle.Bold), ForeColor = Color.FromArgb(32, 96, 191) });
            _cards.Controls.Add(panel);
        }
    }
}
