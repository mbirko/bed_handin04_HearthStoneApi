using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace firstMongoLib.Models;

public class BookModel : ModelBase
{

    [BsonElement("Name")] 
    public string BookName { get; set; } = null!;

    public decimal Price { get; set; }
    public string Category { get; set; } = null!;
    public string Author { get; set; } = null!;

}