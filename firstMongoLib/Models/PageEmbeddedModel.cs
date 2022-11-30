using System.Text.Json;
using System.Text.Json.Nodes;

namespace firstMongoLib.Models;

public class PageEmbeddedModel
{
    public string content { get; set;  }
    public int pageNumber { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}