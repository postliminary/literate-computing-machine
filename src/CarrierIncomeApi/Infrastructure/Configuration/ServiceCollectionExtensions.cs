using CarrierIncomeApi.Domain.Characteristics;
using CarrierIncomeApi.Infrastructure.Repositories;

namespace CarrierIncomeApi.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IContractCharacteristicsRepository, ContractCharacteristicsMockRepository>();
        services.AddSingleton<IProductCharacteristicsRepository, ProductCharacteristicsMockRepository>();
        return services;
    }
}
