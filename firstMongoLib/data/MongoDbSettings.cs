namespace firstMongoLib.data;

public class MongoDbSettings
{
   public string ConnectionString { get; set; } = "mongodb://localhost:27017";
   public string DatabaseName { get; set; } = "BookStore";
   public string CollectionName { get; set; } = "Books";
}
