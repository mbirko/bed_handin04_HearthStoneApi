using firstMongoLib.Models;

namespace Hearthstone_Api.Repositories;

public interface IMongoRepository<T, K>
{
    Task<List<T>> GetAsync();
    Task<T> GetAsync(K id);
    Task CreateAsync(T card);
    Task UpdateAsync(K id, T card);
    Task DeleteAsync(K id);
}