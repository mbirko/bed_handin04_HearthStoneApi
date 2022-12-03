using Hearthstone_Api.DTO;
using Hearthstone_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hearthstone_Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class CardsController : ControllerBase
{

    private readonly ILogger<CardsController> _logger;

    private readonly ICardService _cardService;
    
    public CardsController(
        ILogger<CardsController> logger, 
        ICardService cardsService)
    {
        _logger = logger;
        _cardService = cardsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReturnCard>>> GetAsync(
        [FromQuery] int? page,
        [FromQuery] int? setId,
        [FromQuery] int? classId,
        [FromQuery] int? rarityId,
        [FromQuery] string? artist)
    {
        _logger.LogInformation("{Page}, {Id},{ClassId},{RarityId},{Artist}", page, setId, classId, rarityId, artist);
        return await _cardService.GetReturnCardsByFilterAsync(new CardFilters(setId, classId, rarityId, artist));
    }
}