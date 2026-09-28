using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Infrastructure.Persistence;

internal sealed class EfCommandRepository<TEntity>(FleetDbContext db) : ICommandRepository<TEntity>
    where TEntity : class, IEntity
{
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        db.Set<TEntity>().FindAsync([id], cancellationToken).AsTask();

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken) =>
        await db.Set<TEntity>().AddAsync(entity, cancellationToken);

    public void Remove(TEntity entity) => db.Set<TEntity>().Remove(entity);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            await db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException
            { SqlState: PostgresErrorCodes.UniqueViolation or PostgresErrorCodes.ForeignKeyViolation
                or PostgresErrorCodes.CheckViolation })
        {
            throw new BusinessConflictException("O registro viola uma regra de unicidade ou referência.", ex);
        }
    }
}

internal sealed class EfQueryRepository<TEntity>(FleetDbContext db) : IQueryRepository<TEntity>
    where TEntity : class, IEntity
{
    public Task<TResult?> GetByIdAsync<TResult>(int id, Expression<Func<TEntity, TResult>> projection,
        CancellationToken cancellationToken) =>
        db.Set<TEntity>().AsNoTracking().Where(x => x.Id == id)
            .Select(projection).SingleOrDefaultAsync(cancellationToken);

    public async Task<PagedResult<TResult>> ListAsync<TResult>(PageRequest page,
        Expression<Func<TEntity, TResult>> projection, CancellationToken cancellationToken)
    {
        var offset = page.Offset;
        var query = db.Set<TEntity>().AsNoTracking();
        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderBy(x => x.Id).Skip(offset).Take(page.PageSize)
            .Select(projection).ToListAsync(cancellationToken);
        return new PagedResult<TResult>(items, page.Page, page.PageSize, total);
    }
}
