using firstMongoLib.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Card : ModelBase
    {
        public string Name { get; set; }
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
        public string Artist { get; set; }
        public string Text { get; set; }
        public string FlavorText { get; set; }
    }
}
