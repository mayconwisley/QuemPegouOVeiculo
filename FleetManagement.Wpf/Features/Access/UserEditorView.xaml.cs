using System.Windows;
using System.Windows.Controls;

namespace FleetManagement.Wpf.Features.Access;

public partial class UserEditorView : UserControl
{
    private readonly bool _requireUsername;
    private readonly bool _requirePassword;
    private readonly Func<UserEditorView, Task> _submit;
    private readonly Action _close;

    public UserEditorView(string title, Func<UserEditorView, Task> submit, Action close,
        string? username = null, string? role = null, bool isActive = true,
        bool requireUsername = false, bool requirePassword = false)
    {
        InitializeComponent();
        TitleText.Text = title;
        _submit = submit;
        _close = close;
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

    private async void Save_Click(object sender, RoutedEventArgs e)
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
        try
        {
            SaveButton.IsEnabled = false;
            ErrorText.Visibility = Visibility.Collapsed;
            await _submit(this);
            _close();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
        finally
        {
            SaveButton.IsEnabled = true;
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e) => _close();

    private void ShowError(string message)
    {
        ErrorText.Text = message;
        ErrorText.Visibility = Visibility.Visible;
    }
}
