using Hearthstone_Api.Repositories.Implementations;
using MongoDB.Driver;

namespace Hearthstone_Api.Repositories;

public class SetsRepository : MongoRepository<Domain.Models.Set, int>
{
    public SetsRepository(IMongoDatabase database) : base(database)
    {
    }
}