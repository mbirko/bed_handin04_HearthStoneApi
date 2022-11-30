using Hearthstone_Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hearthstone_Api.Services;

public class CardService : ICardService
{
    private IMongoRepository<Domain.Models.Card, int> _mongoRepository;

    public CardService(IMongoRepository<Domain.Models.Card, int> mongoRepository)
    {
        _mongoRepository = mongoRepository;
    }

    public async Task<ActionResult<List<Domain.Models.Card>>> GetAllCards()
    {
        var cards = await _mongoRepository.GetAsync();

        if (cards == null)
        {
            return new List<Domain.Models.Card>();
        }

        return cards;
    }

    public async Task<ActionResult<Domain.Models.Card>> GetCardById(int id)
    {
        var card = await _mongoRepository.GetAsync(x => x.Id == id);

        if (card == null)
        {
            return new NotFoundResult();
        }

        return card;
    }

    public async Task<ActionResult<Domain.Models.Card>> GetCardBySetId(int id)
    {
        var card = await _mongoRepository.GetAsync(x => x.SetId == id);

        if (card == null)
        {
            return new NotFoundResult();
        }

        return card;
    }
}