namespace CarrierIncomeApi.Domain.Characteristics;

public interface IProductCharacteristicsRepository
{
    Task<ProductCharacteristics?> GetById(string id, CancellationToken cancellationToken);
}
