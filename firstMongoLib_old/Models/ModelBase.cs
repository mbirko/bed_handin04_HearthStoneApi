using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace firstMongoLib.Models;

public class ModelBase
{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id { get; set;  }
}