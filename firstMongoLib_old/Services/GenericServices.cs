using firstMongoLib.data;
using firstMongoLib.Models;
using MongoDB.Driver;

namespace firstMongoLib.Services;

public class GenericServices<ModelType> where ModelType : ModelBase
{ 
   protected readonly IMongoCollection<ModelType> _collection;

   public GenericServices(MongoDbSettings dbSettings)
   {

      var client = new MongoClient(dbSettings.ConnectionString);
      var mongoDatabase = client.GetDatabase(dbSettings.DatabaseName);
      _collection = mongoDatabase.GetCollection<ModelType>(dbSettings.CollectionName);
      
   }
   public virtual async Task<List<ModelType>> GetAsync() 
      => await _collection.Find(_ => true).ToListAsync();

   public virtual async Task<ModelType?> GetAsync(string id) =>
      await _collection.Find(x => x.id == id).FirstOrDefaultAsync();

   public virtual async Task CreateAsync(ModelType book) =>
      await _collection.InsertOneAsync(book);
   
   public virtual async Task UpdateAsync(string id, ModelType book) => 
      await _collection.ReplaceOneAsync(x => x.id == id, book);
   
   public virtual async Task DeleteAsync(string id) =>
      await _collection.DeleteOneAsync(x => x.id == id);
}