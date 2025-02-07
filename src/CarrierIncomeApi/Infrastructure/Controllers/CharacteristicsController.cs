using CarrierIncomeApi.Domain.Characteristics;
using Microsoft.AspNetCore.Mvc;

namespace CarrierIncomeApi.Infrastructure.Controllers;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class CharacteristicsController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IContractCharacteristicsRepository _contractCharacteristicsRepository;
    private readonly IContractCharacteristicsRepository _productCharacteristicsRepository;

    public CharacteristicsController(
        ILogger<CharacteristicsController> logger,
        IContractCharacteristicsRepository contractCharacteristicsRepository,
        IContractCharacteristicsRepository productCharacteristicsRepository
    )
    {
        _logger = logger;
        _contractCharacteristicsRepository = contractCharacteristicsRepository;
        _productCharacteristicsRepository = productCharacteristicsRepository;
    }

    /// <summary>
    /// Get contract characteristics
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Contract/{id}/Characteristics")]
    [ProducesResponseType(typeof(ContractCharacteristics), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContractCharacteristics(string id, CancellationToken cancellationToken)
    {
        var characteristics = await _contractCharacteristicsRepository.GetById(id, cancellationToken);

        if (characteristics is null)
            return NotFound();

        return Ok(characteristics);
    }

    /// <summary>
    /// Get product characteristics
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Product/{id}/Characteristics")]
    [ProducesResponseType(typeof(ProductCharacteristics), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductCharacteristics(string id, CancellationToken cancellationToken)
    {
        var characteristics = await _productCharacteristicsRepository.GetById(id, cancellationToken);

        if (characteristics is null)
            return NotFound();

        return Ok(characteristics);
    }
}
