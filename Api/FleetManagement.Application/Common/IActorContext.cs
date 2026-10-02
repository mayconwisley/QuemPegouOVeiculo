namespace FleetManagement.Application.Common;

public interface IActorContext
{
    int? UserId { get; }
    string Username { get; }
}
