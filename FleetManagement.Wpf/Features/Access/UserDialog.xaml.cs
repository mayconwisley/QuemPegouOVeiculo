using System.Windows;
using System.Windows.Controls;

namespace FleetManagement.Wpf.Features.Access;

public partial class UserDialog : Window
{
    private readonly bool _requireUsername;
    private readonly bool _requirePassword;

    public UserDialog(string title, string? username = null, string? role = null,
        bool isActive = true, bool requireUsername = false, bool requirePassword = false)
    {
        InitializeComponent();
        Title = title;
        _requireUsername = requireUsername;
        _requirePassword = requirePassword;
        UsernameBox.Text = username ?? "";
        UsernameBox.IsReadOnly = !requireUsername;
        ActiveBox.IsChecked = isActive;
        foreach (ComboBoxItem item in RoleBox.Items)
            if ((string)item.Tag == role) RoleBox.SelectedItem = item;

        UsernameLabel.Visibility = UsernameBox.Visibility = requirePassword && !requireUsername
            ? Visibility.Collapsed : Visibility.Visible;
        PasswordLabel.Visibility = PasswordInput.Visibility = requirePassword
            ? Visibility.Visible : Visibility.Collapsed;
        RoleLabel.Visibility = RoleBox.Visibility = ActiveBox.Visibility =
            requirePassword && !requireUsername ? Visibility.Collapsed : Visibility.Visible;
    }

    public string Username => UsernameBox.Text.Trim();
    public string Password => PasswordInput.Password;
    public string Role => (RoleBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "Viewer";
    public bool AccountIsActive => ActiveBox.IsChecked == true;

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (_requireUsername && Username.Length == 0)
        {
            ShowError("Informe o usuário.");
            return;
        }
        if (_requirePassword && Password.Length == 0)
        {
            ShowError("Informe a senha.");
            return;
        }
        DialogResult = true;
    }

    private void ShowError(string message)
    {
        ErrorText.Text = message;
        ErrorText.Visibility = Visibility.Visible;
    }
}
