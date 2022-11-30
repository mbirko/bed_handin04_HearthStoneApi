using firstMongoLib.Models;
using System.Linq.Expressions;

namespace Hearthstone_Api.Repositories;

public interface IMongoRepository<T, K>
{
    Task<List<T>> GetAsync();
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task CreateAsync(T card);
    Task UpdateAsync(K id, T card);
    Task DeleteAsync(K id);
}