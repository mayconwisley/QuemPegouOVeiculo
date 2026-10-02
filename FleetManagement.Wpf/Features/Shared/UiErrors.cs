using System.Net;
using System.Net.Http;
using System.Windows;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf.Features.Shared;

public static class UiErrors
{
    public static void Show(Exception error, string action)
    {
        var message = error is HttpRequestException
            ? "Não foi possível acessar a API. Verifique se o servidor está em execução e a URL configurada."
            : error is TaskCanceledException
                ? "A solicitação demorou demais. Tente novamente."
                : error.Message;
        if (error is ApiException { StatusCode: HttpStatusCode.Unauthorized })
            return;
        MessageBox.Show(message, action, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
