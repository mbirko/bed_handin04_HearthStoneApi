using Hearthstone_Api.Repositories.Implementations;
using MongoDB.Driver;

namespace Hearthstone_Api.Repositories;

public class RaritiesRepository : MongoRepository<Domain.Models.Rarity, int>
{
    public RaritiesRepository(IMongoDatabase database) : base(database)
    {
    }
}