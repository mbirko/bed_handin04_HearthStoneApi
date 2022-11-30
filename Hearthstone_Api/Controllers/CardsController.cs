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
    public async Task<ActionResult<List<Domain.Models.Card>>> GetAsync()
    {
        return await _cardService.GetAllCards();
    }

    [HttpGet("{id:required}")]
    public async Task<ActionResult<Domain.Models.Card>> GetAsync(int id)
    {
        var temp = await _cardService.GetAllCards();

        if (temp == null)
        {
            return NotFound();
        }
        return new Domain.Models.Card();
    }
}