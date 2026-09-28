using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Cadastros;

namespace QuemPegouOVeiculo.Application.Modules.Cadastros.Veiculos;

public sealed record VeiculoInput(string Placa, string Modelo, string? Chassi, string? Renavam, bool Ativo);
public sealed record VeiculoView(int Id, string Placa, string Modelo, string Chassi, string Renavam, bool Ativo);

public sealed class VeiculoCommands(ICommandRepository<Veiculo> repository)
{
    public async Task<Result<int>> CreateAsync(VeiculoInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var veiculo = new Veiculo(input.Placa, input.Modelo, input.Chassi, input.Renavam, input.Ativo);
            await repository.AddAsync(veiculo, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return veiculo.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, VeiculoInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var veiculo = await repository.GetByIdAsync(id, cancellationToken);
            if (veiculo is null)
                return Result.Failure(Error.NotFound("Veículo", id));
            veiculo.Atualizar(input.Placa, input.Modelo, input.Chassi, input.Renavam, input.Ativo);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Veículo", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class VeiculoQueries(IQueryRepository<Veiculo> repository)
{
    private static readonly Expression<Func<Veiculo, VeiculoView>> Projection = x =>
        new VeiculoView(x.Id, x.Placa, x.Modelo, x.Chassi, x.Renavam, x.Ativo);

    public async Task<Result<VeiculoView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<VeiculoView>.Failure(Error.NotFound("Veículo", id))
            : Result<VeiculoView>.Success(item);
    }

    public Task<Result<PagedResult<VeiculoView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
