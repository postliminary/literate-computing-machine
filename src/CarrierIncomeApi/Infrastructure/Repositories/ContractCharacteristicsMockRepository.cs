using CarrierIncomeApi.Domain.Characteristics;

namespace CarrierIncomeApi.Infrastructure.Repositories;

public class ContractCharacteristicsMockRepository : IContractCharacteristicsRepository
{
    public Task<ContractCharacteristics?> GetById(string id, CancellationToken cancellationToken)
    {
        var mock = new ContractCharacteristics
        {
            Id = id,
        };
        return Task.FromResult(mock);
    }
}
