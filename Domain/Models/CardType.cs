using Domain.Models;

namespace Hearthstone_Api.Models
{
    public class CardType : ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
