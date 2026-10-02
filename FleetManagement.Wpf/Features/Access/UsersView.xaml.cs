using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using FleetManagement.Wpf.Features.Shared;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf.Features.Access;

public partial class UsersView : UserControl
{
    private readonly FleetApiClient _api;

    public UsersView(FleetApiClient api)
    {
        InitializeComponent();
        _api = api;
        Loaded += async (_, _) => await RefreshAsync();
    }

    private GridRow? Selected => UsersGrid.SelectedItem as GridRow;

    private async Task RefreshAsync()
    {
        try
        {
            UsersGrid.ItemsSource = (await _api.GetArrayAsync("users"))
                .Select(x => new GridRow(x)).ToArray();
        }
        catch (Exception ex) { UiErrors.Show(ex, "Usuários"); }
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e) => await RefreshAsync();

    private async void New_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new UserDialog("Novo usuário", requireUsername: true,
            requirePassword: true) { Owner = Window.GetWindow(this) };
        if (dialog.ShowDialog() != true) return;
        try
        {
            await _api.SendAsync(HttpMethod.Post, "users", new
            {
                username = dialog.Username,
                password = dialog.Password,
                role = dialog.Role
            });
            await RefreshAsync();
        }
        catch (Exception ex) { UiErrors.Show(ex, "Criar usuário"); }
    }

    private async void Edit_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is not { } row) return;
        var dialog = new UserDialog("Alterar perfil", row["username"], row.Source["role"]?.ToString(),
            row.Source["isActive"]?.GetValue<bool>() == true) { Owner = Window.GetWindow(this) };
        if (dialog.ShowDialog() != true) return;
        try
        {
            await _api.SendAsync(HttpMethod.Put, $"users/{row.Id}", new
            {
                role = dialog.Role,
                isActive = dialog.AccountIsActive
            });
            await RefreshAsync();
        }
        catch (Exception ex) { UiErrors.Show(ex, "Alterar usuário"); }
    }

    private async void Password_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is not { } row) return;
        var dialog = new UserDialog("Redefinir senha", row["username"],
            requirePassword: true) { Owner = Window.GetWindow(this) };
        if (dialog.ShowDialog() != true) return;
        try
        {
            await _api.SendAsync(HttpMethod.Put, $"users/{row.Id}/password",
                new { password = dialog.Password });
            MessageBox.Show("Senha atualizada.", "Usuários", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex) { UiErrors.Show(ex, "Redefinir senha"); }
    }
}
