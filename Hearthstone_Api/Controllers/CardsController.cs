using Hearthstone_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hearthstone_Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class CardsController : ControllerBase
{

    private ILogger<CardsController> _logger;

    private readonly ICardService _cardService;

    public CardsController(ILogger<CardsController> logger, ICardService cardsService)
    {
        _logger = logger;
        _cardService = cardsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Domain.Models.Card>>> GetAsync(
        [FromQuery] int? setId,
        [FromQuery] int? classid,
        [FromQuery] int? rarityid,
        [FromQuery] string? artist)
    {
        _logger.LogInformation($"{setId},{classid},{rarityid},{artist}");
        return await _cardService.GetCardsByFilter(new CardFilters(setId, classid, rarityid, artist));
    }
}