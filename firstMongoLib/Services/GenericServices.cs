using firstMongoLib.data;
using firstMongoLib.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace firstMongoLib.Services;

public class GenericServices<ModelType> : IGenericServices<ModelType> where ModelType : ModelBase
{ 
   protected readonly IMongoCollection<ModelType> _collection;

   public GenericServices(IOptions<MongoDbSettings> dbSettings)
   {
      var client = new MongoClient(dbSettings.Value.ConnectionString);
      var mongoDatabase = client.GetDatabase(dbSettings.Value.DatabaseName);
      _collection = mongoDatabase.GetCollection<ModelType>(dbSettings.Value.CollectionNames["cards"]);
      
   }

   public virtual async Task<List<ModelType>> GetAsync()
      => await _collection.Find(new BsonDocument()).ToListAsync();

   public virtual async Task<ModelType?> GetAsync(int id) =>
      await _collection.Find(_ => true).FirstOrDefaultAsync();

   public virtual async Task CreateAsync(ModelType book) =>
      await _collection.InsertOneAsync(book);
   public virtual async Task UpdateAsync(int id, ModelType book) => 
      await _collection.ReplaceOneAsync(x => x.Id == id, book);
   
   public virtual async Task DeleteAsync(int id) =>
      await _collection.DeleteOneAsync(x => x.Id == id);
}