using System.Text.Json;
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
    
    /// <summary> Embedded model </summary>
    public List<PageEmbeddedModel> Pages { get; set; }

    /// <summary>
    /// Overrides the string method, so that the object can be printed directly
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions(){WriteIndented = true});
    }
}

