using firstMongoLib.data;
using Hearthstone_Api.Models;
using Hearthstone_Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MongoDB.Driver.Core.Operations;

namespace Hearthstone_Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    
    private ILogger<CardsController> _logger;
    private readonly ICardsServices _cardsService;
    public CardsController(ICardsServices cardsService, ILogger<CardsController> logger)
    {
        _cardsService = cardsService;
        _logger = logger;
    }

    [HttpGet("id={id}")]
    public async Task<ActionResult<Card>> GetAsync(int id)
    {
        var temp = await _cardsService.GetAsync(id);
        if (temp == null)
        {
            return NotFound();
        }
        return temp;
    }

    [HttpGet]
    public async Task<ActionResult<List<Card>>> GetAsync()
    {
        var temp = await _cardsService.GetAsync();
        if (temp == null)
        {
            return NotFound();
        }

        return temp;
    }

}