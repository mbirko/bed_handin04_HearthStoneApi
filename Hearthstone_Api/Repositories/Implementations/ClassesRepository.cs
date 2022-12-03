using Hearthstone_Api.Repositories.Implementations;
using MongoDB.Driver;

namespace Hearthstone_Api.Repositories;

public class ClassesRepository : MongoRepository<Domain.Models.Class, int>
{
    public ClassesRepository(IMongoDatabase database) : base(database)
    {
    }
}