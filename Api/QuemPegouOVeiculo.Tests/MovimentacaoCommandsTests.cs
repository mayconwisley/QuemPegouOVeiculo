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

        var result = await commands.CreateAsync(input, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.Conflict, result.Error.Type);
        Assert.False(repository.AddCalled);
    }

    [Fact]
    public async Task ConcludeAsync_RetornaNaoEncontrado()
    {
        var repository = new FakeRepository();
        var commands = new MovimentacaoCommands(repository, new FakeCadastrosStatusReader(true, true));

        var result = await commands.ConcludeAsync(99,
            new ConcluirMovimentacaoInput(new DateTime(2026, 9, 27, 13, 0, 0, DateTimeKind.Utc), 120),
            CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.NotFound, result.Error.Type);
        Assert.Equal("Movimentação 99 não encontrado.", result.Error.Message);
    }

    [Fact]
    public async Task CreateAsync_RetornaReferenciaNaoEncontrada()
    {
        var repository = new FakeRepository();
        var commands = new MovimentacaoCommands(repository, new FakeCadastrosStatusReader(null, true));
        var input = new MovimentacaoInput(99, 2,
            new DateTime(2026, 9, 27, 12, 0, 0, DateTimeKind.Utc), null, 100, null, null);

        var result = await commands.CreateAsync(input, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.NotFound, result.Error.Type);
        Assert.Equal("Veículo 99 não encontrado.", result.Error.Message);
        Assert.False(repository.AddCalled);
    }

    private sealed class FakeCadastrosStatusReader(bool? veiculoAtivo, bool? motoristaAtivo) : ICadastrosStatusReader
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
