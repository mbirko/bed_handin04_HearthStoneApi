

using System.Text.Json.Serialization;

namespace Hearthstone_Api.DTO;
        
public class CardJsonModel
{
        public string Name { get; set; } = null!;
        public int ClassId { get; set; }
        [JsonPropertyName("cardTypeId")]
        public int TypeId { get; set; }
        [JsonPropertyName("cardSetId")]
        public int SetId { get; set; }
        public int? SpellSchoolId { get; set; }
        public int RarityId { get; set; }
        public int? Health { get; set; }
        public int? Attack { get; set; }
        public int ManaCost { get; set; }
        [JsonPropertyName("artistName")]
        public string Artist { get; set; } = null!;

        public string Text { get; set; } = null!;
        public string FlavorText { get; set; } = null!;
}