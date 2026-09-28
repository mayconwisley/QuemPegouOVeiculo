using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Operacoes;
using QuemPegouOVeiculo.Application.Modules.Operacoes.Movimentacoes;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Tests;

public sealed class MovimentacaoCommandsTests
{
    [Fact]
    public async Task CreateAsync_ImpedeUsoDeVeiculoInativo()
    {
        var repository = new FakeRepository();
        var commands = new MovimentacaoCommands(repository, new FakeCadastrosStatusReader(false, true));
        var input = new MovimentacaoInput(1, 2,
            new DateTime(2026, 9, 27, 12, 0, 0, DateTimeKind.Utc), null, 100, null, null);

        await Assert.ThrowsAsync<BusinessConflictException>(() => commands.CreateAsync(input, CancellationToken.None));
        Assert.False(repository.AddCalled);
    }

    private sealed class FakeCadastrosStatusReader(bool veiculoAtivo, bool motoristaAtivo) : ICadastrosStatusReader
    {
        public Task<bool?> IsVeiculoActiveAsync(int id, CancellationToken cancellationToken) =>
            Task.FromResult<bool?>(veiculoAtivo);

        public Task<bool?> IsMotoristaActiveAsync(int id, CancellationToken cancellationToken) =>
            Task.FromResult<bool?>(motoristaAtivo);
    }

    private sealed class FakeRepository : ICommandRepository<MovimentacaoVeiculo>
    {
        public bool AddCalled { get; private set; }

        public Task<MovimentacaoVeiculo?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
            Task.FromResult<MovimentacaoVeiculo?>(null);

        public Task AddAsync(MovimentacaoVeiculo entity, CancellationToken cancellationToken)
        {
            AddCalled = true;
            return Task.CompletedTask;
        }

        public void Remove(MovimentacaoVeiculo entity) { }

        public Task SaveChangesAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
