using MongoDB.Driver;

namespace Hearthstone_Api.Repositories.Implementations;

public class RaritiesRepository : MongoRepository<Domain.Models.Rarity, int>
{
    public RaritiesRepository(IMongoDatabase database) : base(database)
    {
    }
}