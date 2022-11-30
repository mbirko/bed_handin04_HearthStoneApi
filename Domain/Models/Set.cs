using System.Text.Json.Serialization;
using firstMongoLib.Models;

namespace Hearthstone_Api.Models
{
    public class Set : ModelBase
    {
        public String Name { get; set; }
        public String Type { get; set; }
        [JsonPropertyName("collectibleCount")]
        public int CardCount { get; set; }
    }
}
