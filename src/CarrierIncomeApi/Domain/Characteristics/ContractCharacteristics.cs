namespace CarrierIncomeApi.Domain.Characteristics;

public record ContractCharacteristics
{
    /// <summary>
    /// Carrier contract identifier
    /// </summary>
    public string Id { get; init; } = string.Empty;
}
