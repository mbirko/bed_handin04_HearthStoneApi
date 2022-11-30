using MongoDB.Driver;

namespace firstMongoLib.data;

public class MongoDbSettings
{
   public MongoDbSettings(string connectionString, string DatabaseNamespace, string collectionName)
   {
      CollectionName = collectionName;
      ConnectionString = connectionString;
      DatabaseName = DatabaseNamespace;
   }
   public string ConnectionString { get; set; }
   public string DatabaseName { get; set; }
   public string CollectionName { get; set; } 
}
