using Hearthstone_Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hearthstone_Api.Services;

public class CardService : ICardService
{
    private IMongoRepository<Domain.Models.Card, string> _mongoRepository;

    public CardService(IMongoRepository<Domain.Models.Card, string> mongoRepository)
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
}