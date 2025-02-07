namespace CarrierIncomeApi.Domain.Characteristics;

public interface IContractCharacteristicsRepository
{
    Task<ContractCharacteristics?> GetById(string id, CancellationToken cancellationToken);
}
