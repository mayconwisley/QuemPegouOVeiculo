using System.Security.Claims;
using FleetManagement.Application.Common;

namespace FleetManagement.Api.Common;

internal sealed class HttpActorContext(IHttpContextAccessor accessor) : IActorContext
{
    public Guid? UserId => Guid.TryParse(accessor.HttpContext?.User.FindFirstValue("sub"), out var id)
        && id != Guid.Empty ? id : null;
    public string Username => accessor.HttpContext?.User.FindFirstValue("name") ?? "system";
}
