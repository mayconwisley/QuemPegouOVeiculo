using Microsoft.EntityFrameworkCore;
using QuemPegouOVeiculo.Application.Modules.Operacoes;

namespace QuemPegouOVeiculo.Infrastructure.Persistence;

internal sealed class CadastrosStatusReader(FleetDbContext db) : ICadastrosStatusReader
{
    public Task<bool?> IsVeiculoActiveAsync(int id, CancellationToken cancellationToken) =>
        db.Veiculos.AsNoTracking().Where(x => x.Id == id)
            .Select(x => (bool?)x.Ativo).SingleOrDefaultAsync(cancellationToken);

    public Task<bool?> IsMotoristaActiveAsync(int id, CancellationToken cancellationToken) =>
        db.Motoristas.AsNoTracking().Where(x => x.Id == id)
            .Select(x => (bool?)x.Ativo).SingleOrDefaultAsync(cancellationToken);
}
