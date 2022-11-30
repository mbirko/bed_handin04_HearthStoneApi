using Domain.Models;
using firstMongoLib.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Hearthstone_Api.Repositories;

public class CardsRepository : IMongoRepository<Card, int>
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

    public virtual async Task<Card> GetAsync(Expression<Func<Card, bool>> filter)
    {
        return await _database.GetCollection<Card>(collectionName)
            .Find(filter)
            .SingleOrDefaultAsync();
    }

    public virtual async Task CreateAsync(Card card)
    {
        await _database.GetCollection<Card>(collectionName).InsertOneAsync(card);
    }

    public virtual async Task UpdateAsync(int id, Card card)
    {
        await _database.GetCollection<Card>(collectionName).ReplaceOneAsync(x => x.Id == id, card);
    }

    public virtual async Task DeleteAsync(int id)
    {
        await _database.GetCollection<Card>(collectionName).DeleteOneAsync(x => x.Id == id);
    }
}