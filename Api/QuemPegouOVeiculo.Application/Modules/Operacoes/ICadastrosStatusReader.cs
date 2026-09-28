namespace QuemPegouOVeiculo.Application.Modules.Operacoes;

public interface ICadastrosStatusReader
{
    Task<bool?> IsVeiculoActiveAsync(int id, CancellationToken cancellationToken);
    Task<bool?> IsMotoristaActiveAsync(int id, CancellationToken cancellationToken);
}
