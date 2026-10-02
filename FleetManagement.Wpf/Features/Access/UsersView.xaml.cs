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

    private void ShowEditor(UserEditorView editor)
    {
        EditorHost.Content = editor;
        ListContent.Visibility = Visibility.Collapsed;
        EditorHost.Visibility = Visibility.Visible;
    }

    private void CloseEditor()
    {
        EditorHost.Content = null;
        EditorHost.Visibility = Visibility.Collapsed;
        ListContent.Visibility = Visibility.Visible;
    }

    private async Task RefreshAsync()
    {
        try
        {
            EmptyState.Visibility = Visibility.Collapsed;
            UsersGrid.ItemsSource = (await _api.GetArrayAsync("users"))
                .Select(x => new GridRow(x)).ToArray();
            EmptyState.Visibility = UsersGrid.Items.Count == 0
                ? Visibility.Visible : Visibility.Collapsed;
        }
        catch (Exception ex)
        {
            EmptyState.Visibility = Visibility.Collapsed;
            UiErrors.Show(ex, "Usuários");
        }
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e) => await RefreshAsync();

    private void New_Click(object sender, RoutedEventArgs e)
    {
        ShowEditor(new UserEditorView("Novo usuário", async editor =>
        {
            await _api.SendAsync(HttpMethod.Post, "users", new
            {
                username = editor.Username,
                password = editor.Password,
                role = editor.Role
            });
            await RefreshAsync();
            StatusText.Text = "Usuário criado.";
        }, CloseEditor, requireUsername: true, requirePassword: true));
    }

    private void Edit_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is not { } row) return;
        ShowEditor(new UserEditorView("Alterar perfil", async editor =>
        {
            await _api.SendAsync(HttpMethod.Put, $"users/{row.Id}", new
            {
                role = editor.Role,
                isActive = editor.AccountIsActive
            });
            await RefreshAsync();
            StatusText.Text = "Perfil atualizado.";
        }, CloseEditor, row["username"], row.Source["role"]?.ToString(),
            row.Source["isActive"]?.GetValue<bool>() == true));
    }

    private void Password_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is not { } row) return;
        ShowEditor(new UserEditorView("Redefinir senha", async editor =>
        {
            await _api.SendAsync(HttpMethod.Put, $"users/{row.Id}/password",
                new { password = editor.Password });
            StatusText.Text = "Senha atualizada.";
        }, CloseEditor, row["username"], requirePassword: true));
    }
}
