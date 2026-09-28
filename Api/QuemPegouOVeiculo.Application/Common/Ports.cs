using System.Linq.Expressions;
using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Application.Common;

public interface ICommandRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Remove(TEntity entity);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}

public interface IQueryRepository<TEntity> where TEntity : class, IEntity
{
    Task<TResult?> GetByIdAsync<TResult>(int id, Expression<Func<TEntity, TResult>> projection,
        CancellationToken cancellationToken);
    Task<PagedResult<TResult>> ListAsync<TResult>(PageRequest page, Expression<Func<TEntity, TResult>> projection,
        CancellationToken cancellationToken);
}

public sealed record PageRequest(int Page = 1, int PageSize = 50)
{
    public int Offset
    {
        get
        {
            var offset = ((long)Page - 1) * PageSize;
            if (Page < 1 || PageSize is < 1 or > 100 || offset > int.MaxValue)
                throw new DomainException("Página inválida. page deve ser positivo e pageSize deve estar entre 1 e 100.");
            return (int)offset;
        }
    }
}

public sealed record PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int Total);

public sealed class EntityNotFoundException(string entity, int id)
    : Exception($"{entity} {id} não encontrado.");

public sealed class BusinessConflictException(string message, Exception? innerException = null)
    : Exception(message, innerException);

public static class CommandRepositoryExtensions
{
    public static async Task<TEntity> GetRequiredAsync<TEntity>(this ICommandRepository<TEntity> repository,
        int id, string entityName, CancellationToken cancellationToken) where TEntity : class, IEntity =>
        await repository.GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException(entityName, id);
}
