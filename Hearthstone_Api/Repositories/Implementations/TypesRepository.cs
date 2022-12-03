using MongoDB.Driver;

namespace Hearthstone_Api.Repositories.Implementations;

public class TypesRepository : MongoRepository<Domain.Models.CardType, int>
{
    public TypesRepository(IMongoDatabase database) : base(database)
    {
    }
}