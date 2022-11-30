using firstMongoLib.data;

namespace Hearthstone_Api.Data;

public class HearthstoneDbSettings : MongoDbSettings
{
   public string CollectionName { get; set; } = null!;
}