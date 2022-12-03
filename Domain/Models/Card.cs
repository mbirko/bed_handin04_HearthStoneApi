using MongoDB.Bson.Serialization.Attributes;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Domain.Models
{
    public class Card : ModelBase<int>
    {
        public string Name { get; set; } = null!;
        public int ClassId { get; set; }
        [BsonElement("cardTypeId")]
        public int TypeId { get; set; }
        [BsonElement("cardSetId")]
        public int SetId { get; set; }
        public int? SpellSchoolId { get; set; }
        public int RarityId { get; set; }
        public int? Health { get; set; }
        public int? Attack { get; set; }
        public int ManaCost { get; set; }
        [BsonElement("artistName")]
        public string Artist { get; set; } = null!;

        public string Text { get; set; } = null!;
        public string FlavorText { get; set; } = null!;
    }
}
