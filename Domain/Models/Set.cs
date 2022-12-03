using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Set : ModelBase<int>
    {
        public String Name { get; set; } = null!;
        public String Type { get; set; } = null!;
        [JsonPropertyName("collectibleCount")]
        public int CardCount { get; set; }
    }
}
