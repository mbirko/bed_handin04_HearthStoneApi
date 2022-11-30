using firstMongoLib.data;
using firstMongoLib.Models;
using MongoDB.Driver;

namespace firstMongoLib.Services;

public class BookServices : GenericServices<BookModel>
{
    public BookServices(MongoDbSettings dbSettings) : base(dbSettings)
    {
    }
}
