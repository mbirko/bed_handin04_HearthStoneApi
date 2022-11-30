using firstMongoLib.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Hearthstone_Api.Models
{
    public class Card : ModelBase
    {
        public String Name { get; set; }
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
        public String Artist { get; set; }
        public String Text { get; set; }
        public String FlavorText { get; set; }
    }
}
