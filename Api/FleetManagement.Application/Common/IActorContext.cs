namespace FleetManagement.Application.Common;

public interface IActorContext
{
    Guid? UserId { get; }
    string Username { get; }
}
