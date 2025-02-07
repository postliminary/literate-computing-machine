namespace CarrierIncomeApi.Domain.Characteristics;

public record ProductCharacteristics
{
    /// <summary>
    /// Carrier product identifier
    /// </summary>
    public string Id { get; init; } = string.Empty;
}
