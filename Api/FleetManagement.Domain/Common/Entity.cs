namespace FleetManagement.Domain.Common;

public interface IEntity
{
    Guid Id { get; }
}

public sealed class DomainException(string message) : Exception(message);
