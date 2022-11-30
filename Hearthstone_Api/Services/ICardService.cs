using Microsoft.AspNetCore.Mvc;

namespace Hearthstone_Api.Services
{
    public interface ICardService 
    {
        Task<ActionResult<List<Domain.Models.Card>>> GetAllCards();

        Task<ActionResult<Domain.Models.Card>> GetCardById(int id);
    }
}
