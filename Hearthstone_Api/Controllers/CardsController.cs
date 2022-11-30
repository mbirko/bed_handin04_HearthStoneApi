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
    public async Task<ActionResult<Domain.Models.Card>> GetByIdAsync(int id)
    {
        return await _cardService.GetCardById(id);
    }

    [HttpGet("{setId:required}")]
    public async Task<ActionResult<Domain.Models.Card>> GetBySetIdAsync(int setId)
    {
        return await _cardService.GetCardById(setId);
    }
}