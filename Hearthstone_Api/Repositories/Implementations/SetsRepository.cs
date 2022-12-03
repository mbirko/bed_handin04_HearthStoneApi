using MongoDB.Driver;

namespace Hearthstone_Api.Repositories.Implementations;

public class SetsRepository : MongoRepository<Domain.Models.Set, int>
{
    public SetsRepository(IMongoDatabase database) : base(database)
    {
    }
}