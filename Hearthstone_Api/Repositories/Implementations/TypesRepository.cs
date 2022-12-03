using Hearthstone_Api.Repositories.Implementations;
using MongoDB.Driver;

namespace Hearthstone_Api.Repositories;

public class TypesRepository : MongoRepository<Domain.Models.CardType, int>
{
    public TypesRepository(IMongoDatabase database) : base(database)
    {
    }
}