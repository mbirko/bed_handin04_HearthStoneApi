using firstMongoLib.data;
using firstMongoLib.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace firstMongoLib.Services;

public class BookServices : GenericServices<BookModel>
{
    public BookServices(IOptions<MongoDbSettings> dbSettings) : base(dbSettings )
    {
        
    }

    public async Task<BookModel> GetAsync(string author)
    {
        return await _collection.Find(t => t.Author == author).FirstOrDefaultAsync();
    }

}
