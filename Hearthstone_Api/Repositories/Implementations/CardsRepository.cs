using Hearthstone_Api.Repositories.Implementations;
using MongoDB.Driver;

namespace Hearthstone_Api.Repositories;

public class CardsRepository 
    : MongoRepository<Domain.Models.Card, int>
{
    public CardsRepository(IMongoDatabase database) : base(database)
    {

    }

}