using CarrierIncomeApi.Domain.Characteristics;

namespace CarrierIncomeApi.Infrastructure.Repositories;

public class ProductCharacteristicsMockRepository : IProductCharacteristicsRepository
{
    public Task<ProductCharacteristics?> GetById(string id, CancellationToken cancellationToken)
    {
        var mock = new ProductCharacteristics
        {
            Id = id,
        };
        return Task.FromResult(mock);
    }
}
