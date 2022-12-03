using MongoDB.Driver;

namespace Hearthstone_Api.Repositories.Implementations;

public class MongoRepository<TM, TK> : IMongoRepository<TM, TK>
    where TM : Domain.Models.ModelBase<TK>
{
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly IMongoDatabase Database;
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly string CollectionName;
    
    public MongoRepository(IMongoDatabase database)
    {
        Database = database;
        CollectionName = GetType().ToString();
    }

    public override string ToString()
    {
        return CollectionName;
    }

    public virtual async Task<TM> GetAsync(TK id)
    {
        return await Database.GetCollection<TM>(CollectionName)
            .Find(x => x.Id!.Equals(id)).SingleOrDefaultAsync();
    }
    public virtual async Task<List<TM>> GetAsync(FilterDefinition<TM>? filter = null)
    {
        filter ??= FilterDefinition<TM>.Empty;
        return await Database.GetCollection<TM>(CollectionName)
                .Find(filter)
                .ToListAsync();
    }

    public virtual async Task CreateAsync(TM model)
    {
        await Database.GetCollection<TM>(CollectionName)
            .InsertOneAsync(model);
    }
    public virtual async Task CreateManyAsync(List<TM> model)
    {
        await Database.GetCollection<TM>(CollectionName)
            .InsertManyAsync(model);
    }

    public virtual async Task UpdateAsync(TK id, TM card)
    {
        await Database.GetCollection<TM>(CollectionName)
            .ReplaceOneAsync(x => x.Id!.Equals(id), card);
    }

    public virtual async Task DeleteAsync(TK id)
    {
        await Database.GetCollection<TM>(CollectionName)
            .DeleteOneAsync(x => x.Id!.Equals(id));
    }

    public virtual async Task<long> Count(FilterDefinition<TM>? filter = null)
    {
        filter ??= FilterDefinition<TM>.Empty;
        return await Database.GetCollection<TM>(CollectionName).CountDocumentsAsync(filter);
    }
}