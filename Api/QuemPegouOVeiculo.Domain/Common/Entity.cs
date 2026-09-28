namespace QuemPegouOVeiculo.Domain.Common;

public interface IEntity
{
    int Id { get; }
}

public sealed class DomainException(string message) : Exception(message);
