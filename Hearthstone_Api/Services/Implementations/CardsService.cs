using Domain.Models;
using Hearthstone_Api.DTO;
using Hearthstone_Api.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

// ReSharper disable once CheckNamespace
namespace Hearthstone_Api.Services;

public class CardFilters
{
    public CardFilters(int? setId, int? classId, int? rarityId, string? artist)
    {
        SetId = setId;
        ClassId = classId;
        RarityId = rarityId;
        Artist = artist;
    }

    public int? SetId { get; set; }
    public int? ClassId { get; set; }
    public int? RarityId { get; set; }
    public string? Artist { get; set; }
}

public class CardService : RepositoryService<Domain.Models.Card, int>, ICardService
{
    private readonly IMongoRepository<Domain.Models.Class, int> _classRepository; 
    private readonly IMongoRepository<Domain.Models.CardType, int> _typesRepository; 
    private readonly IMongoRepository<Domain.Models.Set, int> _setsRepository; 
    private readonly IMongoRepository<Domain.Models.Rarity, int> _raritiesRepository; 
    public CardService(
        IMongoRepository<Domain.Models.Card, int> mongoRepository, 
        IMongoRepository<Domain.Models.Class, int> classRepository, 
        IMongoRepository<Domain.Models.CardType, int> typesRepository, 
        IMongoRepository<Domain.Models.Set, int> setsRepository, 
        IMongoRepository<Domain.Models.Rarity, int> raritiesRepository) : base(mongoRepository)
    {
        _classRepository = classRepository;
        _typesRepository = typesRepository;
        _setsRepository = setsRepository;
        _raritiesRepository = raritiesRepository;
    }

    public async Task<ActionResult<List<ReturnCard>>> GetReturnCardsByFilterAsync(CardFilters cardFilter)
    {
        var cards = await GetCardsByFilterAsync(cardFilter);
        List<ReturnCard>? returnCards = new List<ReturnCard>();
        int i = 0; 
        foreach (var card in cards.Value)
        {
            returnCards.Add(new ReturnCard());
            card.Adapt(returnCards[i]);
            var type = await _typesRepository.GetAsync(card.TypeId);
            returnCards[i].Type = type.Name;
            var class_ = await _classRepository.GetAsync(card.ClassId);
            returnCards[i].Class = class_.Name;
            var set = await _setsRepository.GetAsync(card.SetId);
            returnCards[i].Set = set.Name;
            var rarity = await _raritiesRepository.GetAsync(card.RarityId);
            returnCards[i].Rarity = rarity.Name;
            i++;
        }

        return returnCards;

    }
    public async Task<ActionResult<List<Domain.Models.Card>>> GetCardsByFilterAsync(CardFilters cardFilters)
    {
        var mongoFilter = CreateFilter(cardFilters);

        var cards = await _repository.GetAsync(mongoFilter);

        if (cards == null)
        {
            return new List<Domain.Models.Card>();
        }

        return cards;
    }

    private FilterDefinition<Card>? CreateFilter(CardFilters cardFilters)
    {
        var builder = Builders<Domain.Models.Card>.Filter;
        var mongoFilter = builder.Empty;

        if (cardFilters.SetId.HasValue)
        {
            var setFilter = builder.Where(x => x.SetId == cardFilters.SetId.Value);
            mongoFilter &= setFilter;
        }

        if (cardFilters.ClassId.HasValue)
        {
            var classFilter = builder.Where(x => x.ClassId == cardFilters.ClassId.Value);
            mongoFilter &= classFilter;
        }

        if (cardFilters.RarityId.HasValue)
        {
            var rarityFilter = builder.Where(x => x.RarityId == cardFilters.RarityId.Value);
            mongoFilter &= rarityFilter;
        }

        if (!string.IsNullOrEmpty(cardFilters.Artist))
        {
            var artistFilter = builder.Where(x => x.Artist == cardFilters.Artist);
            mongoFilter &= artistFilter;
        }

        return mongoFilter;
    }

}