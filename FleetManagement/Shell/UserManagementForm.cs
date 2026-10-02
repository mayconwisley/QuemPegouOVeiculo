using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement
{
    public sealed class UserManagementForm : Form
    {
        private readonly DataGridView _grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true,
            AutoGenerateColumns = false, AllowUserToAddRows = false, RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
        private readonly TextBox _username = new TextBox { Width = 150 };
        private readonly TextBox _password = new TextBox { Width = 180, UseSystemPasswordChar = true };
        private readonly ComboBox _role = new ComboBox { Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly CheckBox _active = new CheckBox { Text = "Ativo", Checked = true, AutoSize = true };
        private readonly Label _message = new Label { AutoSize = true };

        public UserManagementForm()
        {
            Text = "Usuários e permissões";
            MinimumSize = new Size(780, 480);
            Font = new Font("Segoe UI", 9F);
            _role.Items.AddRange(new object[] { "Administrador", "Operador", "Consulta" });
            _role.SelectedIndex = 1;

            var input = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(12, 10, 0, 0) };
            input.Controls.Add(new Label { Text = "Usuário", AutoSize = true });
            input.Controls.Add(_username);
            input.Controls.Add(new Label { Text = "Senha", AutoSize = true });
            input.Controls.Add(_password);
            input.Controls.Add(_role);
            input.Controls.Add(_active);

            var actions = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 46, Padding = new Padding(12, 6, 0, 0) };
            AddButton(actions, "Novo", () =>
            {
                _username.Clear();
                _password.Clear();
                _role.SelectedIndex = 1;
                _active.Checked = true;
                _username.Focus();
                return Task.CompletedTask;
            });
            AddButton(actions, "Criar", async () => await CreateAsync());
            AddButton(actions, "Atualizar perfil", async () => await UpdateAsync());
            AddButton(actions, "Redefinir senha", async () => await ResetPasswordAsync());
            AddButton(actions, "Recarregar", async () => await LoadUsersAsync());
            actions.Controls.Add(_message);

            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Usuário", DataPropertyName = "Username",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Perfil", DataPropertyName = "Role", Width = 140 });
            _grid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Ativo", DataPropertyName = "IsActive", Width = 80 });
            _grid.CellFormatting += (sender, args) =>
            {
                if (args.ColumnIndex != 1 || args.Value == null) return;
                args.Value = (string)args.Value == "Administrator" ? "Administrador" :
                    (string)args.Value == "Operator" ? "Operador" : "Consulta";
                args.FormattingApplied = true;
            };
            _grid.SelectionChanged += (sender, args) =>
            {
                var selected = Selected;
                if (selected == null) return;
                _username.Text = selected.Username;
                _role.SelectedIndex = selected.Role == "Administrator" ? 0 : selected.Role == "Operator" ? 1 : 2;
                _active.Checked = selected.IsActive;
                _password.Clear();
            };
            Controls.Add(_grid);
            Controls.Add(actions);
            Controls.Add(input);
            Shown += async (sender, args) => await LoadUsersAsync();
        }

        private UserRecord Selected => _grid.CurrentRow?.DataBoundItem as UserRecord;
        private string Role => _role.SelectedIndex == 0 ? "Administrator" : _role.SelectedIndex == 1 ? "Operator" : "Viewer";

        private static void AddButton(Control parent, string text, Func<Task> action)
        {
            var button = new Button { Text = text, AutoSize = true };
            button.Click += async (sender, args) =>
            {
                button.Enabled = false;
                try { await action(); }
                finally { button.Enabled = true; }
            };
            parent.Controls.Add(button);
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                _grid.DataSource = (await AccessClient.ListUsersAsync()).ToList();
                _message.Text = "";
            }
            catch (Exception exception) { _message.Text = exception.Message; }
        }

        private async Task CreateAsync()
        {
            try
            {
                await AccessClient.CreateUserAsync(_username.Text.Trim(), _password.Text, Role);
                _password.Clear();
                await LoadUsersAsync();
            }
            catch (Exception exception) { _message.Text = exception.Message; }
        }

        private async Task UpdateAsync()
        {
            if (Selected == null) return;
            try
            {
                await AccessClient.UpdateUserAsync(Selected.Id, Role, _active.Checked);
                await LoadUsersAsync();
            }
            catch (Exception exception) { _message.Text = exception.Message; }
        }

        private async Task ResetPasswordAsync()
        {
            if (Selected == null) return;
            try
            {
                await AccessClient.ResetPasswordAsync(Selected.Id, _password.Text);
                _password.Clear();
                _message.Text = "Senha redefinida.";
            }
            catch (Exception exception) { _message.Text = exception.Message; }
        }
    }
}
