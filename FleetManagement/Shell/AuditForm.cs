using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement
{
    public sealed class AuditForm : Form
    {
        private readonly DataGridView _grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true,
            AutoGenerateColumns = false, AllowUserToAddRows = false, RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect };
        private readonly Button _previous = new Button { Text = "Anterior" };
        private readonly Button _next = new Button { Text = "Próxima" };
        private readonly Label _state = new Label { AutoSize = true };
        private int _page = 1;

        public AuditForm()
        {
            Text = "Histórico de alterações";
            MinimumSize = new Size(850, 480);
            Font = new Font("Segoe UI", 9F);
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Data UTC", DataPropertyName = "OccurredAtUtc", Width = 150 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Usuário", DataPropertyName = "ActorUsername", Width = 130 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Entidade", DataPropertyName = "EntityLabel", Width = 150 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "EntityId", Width = 65 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ação", DataPropertyName = "ActionLabel", Width = 90 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Alterações", DataPropertyName = "Summary",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 46, Padding = new Padding(12, 8, 0, 0) };
            _previous.Click += async (sender, args) => { if (_page > 1) { _page--; await LoadAsync(); } };
            _next.Click += async (sender, args) => { _page++; await LoadAsync(); };
            toolbar.Controls.Add(_previous);
            toolbar.Controls.Add(_next);
            toolbar.Controls.Add(_state);
            Controls.Add(_grid);
            Controls.Add(toolbar);
            Shown += async (sender, args) => await LoadAsync();
        }

        private async Task LoadAsync()
        {
            _previous.Enabled = _next.Enabled = false;
            try
            {
                var page = await AccessClient.GetAuditAsync(_page);
                _grid.DataSource = page.Items.ToList();
                _state.Text = string.Format("Página {0} · {1} registros", _page, page.Total);
                _previous.Enabled = _page > 1;
                _next.Enabled = _page * 100 < page.Total;
            }
            catch (Exception exception) { _state.Text = exception.Message; }
        }
    }
}
