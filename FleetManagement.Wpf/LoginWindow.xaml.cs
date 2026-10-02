using System.Net.Http;
using System.Windows;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf;

public partial class LoginWindow : Window
{
    private readonly FleetApiClient _api;

    public LoginWindow(FleetApiClient api)
    {
        InitializeComponent();
        _api = api;
        UsernameBox.Focus();
    }

    private async void Enter_Click(object sender, RoutedEventArgs e)
    {
        var username = UsernameBox.Text.Trim();
        var password = PasswordInput.Password;
        if (username.Length == 0 || password.Length == 0)
        {
            ShowError("Informe usuário e senha.");
            return;
        }

        EnterButton.IsEnabled = false;
        ErrorText.Visibility = Visibility.Collapsed;
        try
        {
            await _api.LoginAsync(username, password);
            DialogResult = true;
        }
        catch (HttpRequestException)
        {
            ShowError("Não foi possível acessar a API. Verifique se o servidor está em execução.");
        }
        catch (TaskCanceledException)
        {
            ShowError("A conexão com a API demorou demais. Tente novamente.");
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
        finally
        {
            EnterButton.IsEnabled = true;
        }
    }

    private void ShowError(string text)
    {
        ErrorText.Text = text;
        ErrorText.Visibility = Visibility.Visible;
    }
}
