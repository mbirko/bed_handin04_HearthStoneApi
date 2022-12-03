using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace Hearthstone_Api.DTO;

public class MetaData
{   
    public List<Set> Sets { get; set; }
    public List<Rarity> Rarities { get; set; }
    public List<Class> Classes { get; set; }
    public List<CardType> Types { get; set; }
}

public class Set
{
    
    public int Id { get; set; }
    public String Name { get; set; }
    public String Type { get; set; }
    [JsonPropertyName("collectibleCount")]
    public int CardCount { get; set; }
}

public class Rarity 
{
    public int Id { get; set; }
    public String Name { get; set; }
}
public class Class 
{
    public int Id { get; set; }
    public String Name { get; set; }
}

public class CardType 
{
    public int Id { get; set; }
    public string Name { get; set; }
}
