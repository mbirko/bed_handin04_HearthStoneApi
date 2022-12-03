using MongoDB.Driver;

namespace Hearthstone_Api.Repositories;

// ReSharper disable once TypeParameterCanBeVariant
public interface IMongoRepository<T, TK>
{
    Task<T> GetAsync(TK id);
    Task<List<T>> GetAsync(FilterDefinition<T>? filter = null);
    Task CreateAsync(T model);
    Task CreateManyAsync(List<T> model);
    Task UpdateAsync(TK id, T model);
    Task DeleteAsync(TK id);
    Task<long> Count(FilterDefinition<T>? filter = null);

}