using Hearthstone_Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Hearthstone_Api.Services;

public class CardFilters
{
    public CardFilters(int? setId, int? classid, int? rarityid, string? artist)
    {
        this.setId = setId;
        this.classid = classid;
        this.rarityid = rarityid;
        this.artist = artist;
    }

    public int? setId { get; set; }
    public int? classid { get; set; }
    public int? rarityid { get; set; }
    public string? artist { get; set; }
}

public class CardService : ICardService
{
    private IMongoRepository<Domain.Models.Card, int> _mongoRepository;

    public CardService(IMongoRepository<Domain.Models.Card, int> mongoRepository)
    {
        _mongoRepository = mongoRepository;
    }

    public async Task<ActionResult<List<Domain.Models.Card>>> GetCardsByFilter(CardFilters cardFilters)
    {
        var mongoFilter = CreateFilter(cardFilters);

        var cards = await _mongoRepository.GetAsync(mongoFilter);

        if (cards == null)
        {
            return new List<Domain.Models.Card>();
        }

        return cards;
    }

    public FilterDefinition<Domain.Models.Card> CreateFilter(CardFilters cardFilters)
    {
        var builder = Builders<Domain.Models.Card>.Filter;
        var mongoFilter = builder.Empty;

        if (cardFilters.setId.HasValue)
        {
            var setFilter = builder.Where(x => x.SetId == cardFilters.setId.Value);
            mongoFilter &= setFilter;
        }

        if (cardFilters.classid.HasValue)
        {
            var classFilter = builder.Where(x => x.ClassId == cardFilters.classid.Value);
            mongoFilter &= classFilter;
        }

        if (cardFilters.rarityid.HasValue)
        {
            var rarityFilter = builder.Where(x => x.RarityId == cardFilters.rarityid.Value);
            mongoFilter &= rarityFilter;
        }

        if (!string.IsNullOrEmpty(cardFilters.artist))
        {
            var artistFilter = builder.Where(x => x.Artist == cardFilters.artist);
            mongoFilter &= artistFilter;
        }

        return mongoFilter;
    }
}