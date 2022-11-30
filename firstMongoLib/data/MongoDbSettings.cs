namespace firstMongoLib.data;

public class MongoDbSettings
{
   public string ConnectionString { get; set; } = null!;
   public string DatabaseName { get; set; } = null!;
   
   public Dictionary<string, string> CollectionNames { get; set; } = null!;

   //public string CollectionItem { get; set; } = "Items";
}
