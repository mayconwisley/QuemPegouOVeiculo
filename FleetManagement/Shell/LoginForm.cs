using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement
{
    public sealed class LoginForm : Form
    {
        private readonly TextBox _username = new TextBox();
        private readonly TextBox _password = new TextBox { UseSystemPasswordChar = true };
        private readonly Button _submit = new Button { Text = "Entrar" };
        private readonly Label _message = new Label { AutoSize = false, ForeColor = Color.Firebrick };

        public LoginForm()
        {
            Text = "Acesso ao controle da frota";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(380, 245);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            Controls.Add(new Label { Text = "Usuário", Location = new Point(28, 20), AutoSize = true });
            _username.SetBounds(28, 47, 324, 28);
            Controls.Add(_username);
            Controls.Add(new Label { Text = "Senha", Location = new Point(28, 86), AutoSize = true });
            _password.SetBounds(28, 113, 324, 28);
            Controls.Add(_password);
            _submit.SetBounds(252, 159, 100, 34);
            _submit.Click += async (sender, args) => await SignInAsync();
            Controls.Add(_submit);
            _message.SetBounds(28, 203, 324, 36);
            Controls.Add(_message);
            AcceptButton = _submit;
        }

        private async Task SignInAsync()
        {
            _submit.Enabled = false;
            _message.Text = "Conectando...";
            try
            {
                await AccessClient.LoginAsync(_username.Text, _password.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                _message.Text = exception.Message;
                _password.SelectAll();
                _password.Focus();
            }
            finally { _submit.Enabled = true; }
        }
    }
}
