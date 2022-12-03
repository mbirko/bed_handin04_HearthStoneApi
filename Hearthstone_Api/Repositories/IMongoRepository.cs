using MongoDB.Driver;
using System.Linq.Expressions;

namespace Hearthstone_Api.Repositories;

public interface IMongoRepository<T, K>
{
    Task<T> GetAsync(K id);
    Task<List<T>> GetAsync(FilterDefinition<T>? filter = null);
    Task CreateAsync(T model);
    Task CreateManyAsync(List<T> model);
    Task UpdateAsync(K id, T model);
    Task DeleteAsync(K id);
    Task<long> Count(FilterDefinition<T>? filter = null);

}