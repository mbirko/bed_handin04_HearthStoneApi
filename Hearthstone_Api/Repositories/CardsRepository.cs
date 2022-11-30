using Domain.Models;
using firstMongoLib.Models;
using MongoDB.Driver;

namespace Hearthstone_Api.Repositories;

public class CardsRepository : IMongoRepository<Card, string>
{
    // TODO: virk på alle collections, og ikke kun cards.
    protected readonly IMongoDatabase _database;
    private const string collectionName = "cards";

    public CardsRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public virtual async Task<List<Card>> GetAsync()
    {
        return await _database.GetCollection<Card>(collectionName)
            .Find(_ => true)
            .ToListAsync();
    }

    public virtual async Task<Card?> GetAsync(string id)
    {
        return await _database.GetCollection<Card>(collectionName)
            .Find(x => x._id == id)
            .SingleOrDefaultAsync();
    }

    public virtual async Task CreateAsync(Card card)
    {
        await _database.GetCollection<Card>(collectionName).InsertOneAsync(card);
    }

    public virtual async Task UpdateAsync(string id, Card card)
    {
        await _database.GetCollection<Card>(collectionName).ReplaceOneAsync(x => x._id == id, card);
    }

    public virtual async Task DeleteAsync(string id)
    {
        await _database.GetCollection<Card>(collectionName).DeleteOneAsync(x => x._id == id);
    }
}