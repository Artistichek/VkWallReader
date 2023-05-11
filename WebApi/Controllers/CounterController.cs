using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VkWallReader.BLL.Dto;
using VkWallReader.BLL.Interfaces;
using VkWallReader.BLL.Services;
using VkWallReader.DAL.Commands.AddCountedWall;

namespace VkWallReader.WebApi.Controllers;

[Route("api/count")]
public class CounterController : ControllerBase
{
    private readonly IReaderService _readerService;
    private readonly ICounterService _counterService;
    private readonly ILogger<CounterService> _logger;
    private readonly IAddCountedWallCommandHandler _addCountedWall;

    public CounterController(
        IReaderService readerService, 
        ICounterService counterService,
        ILogger<CounterService> logger,
        IAddCountedWallCommandHandler addCountedWall)
    {
        _readerService = readerService;
        _counterService = counterService;
        _logger = logger;
        _addCountedWall = addCountedWall;
    }

    /// <summary>
    /// Gets text content from vk wall and counts letters
    /// </summary>
    /// <param name="domain">Address of personal page</param>
    /// <returns>Counted letters and wall text</returns>
    /// <response code="201">Success</response>
    /// <response code="400">If VK API returned any error</response>
    /// <response code="409">If counted letters for this domain already exist</response>
    [HttpGet("{domain}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CountedWallDto>> CountLettersAsync(string domain)
    {
        var wallContent = await _readerService.ReadData(domain);

        var wallText = wallContent.Items
            .Select(x => x.Text)
            .Aggregate((a, b) => a + b)
            .ToLower();
        
        _logger.LogInformation($"Counting letters on page: {domain} ");
        wallContent.CountedLetters = await _counterService.CountLetters(wallText);
        _logger.LogInformation("Letters were counted");
        
        var model = new AddCountedWallModel
        {
            CountedLetters = JsonDocument.Parse(JsonConvert
                .SerializeObject(wallContent.CountedLetters)),
            Domain = wallContent.Domain,
            Hash = wallContent.GetHashCode()
        };
        
        await _addCountedWall.AddAsync(model);
        _logger.LogInformation("Result was added to DB");
        
        return Ok(wallContent);
    }
    
}
