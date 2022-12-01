using MongoDB.Driver;

namespace Hearthstone_Api.Repositories;

public class CardsRepository : IMongoRepository<Domain.Models.Card, int>
{
    // TODO: virk på alle collections, og ikke kun cards.
    protected readonly IMongoDatabase _database;
    private const string collectionName = "cards";

    public CardsRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public virtual async Task<List<Domain.Models.Card>> GetAsync(FilterDefinition<Domain.Models.Card> filter)
    {
        return await _database.GetCollection<Domain.Models.Card>(collectionName)
                .Find(filter)
                .ToListAsync();

    }

    public virtual async Task CreateAsync(Domain.Models.Card card)
    {
        await _database.GetCollection<Domain.Models.Card>(collectionName).InsertOneAsync(card);
    }

    public virtual async Task UpdateAsync(int id, Domain.Models.Card card)
    {
        await _database.GetCollection<Domain.Models.Card>(collectionName).ReplaceOneAsync(x => x.Id == id, card);
    }

    public virtual async Task DeleteAsync(int id)
    {
        await _database.GetCollection<Domain.Models.Card>(collectionName).DeleteOneAsync(x => x.Id == id);
    }
}