using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
// ReSharper disable InconsistentNaming

namespace Domain.Models;

public abstract class ModelBase<TK>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; } = null!;

    public TK? Id { get; set; }


}