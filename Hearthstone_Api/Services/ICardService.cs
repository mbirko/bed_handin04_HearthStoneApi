using Microsoft.AspNetCore.Mvc;

namespace Hearthstone_Api.Services
{
    public interface ICardService 
    {
        Task<ActionResult<List<Domain.Models.Card>>> GetAllCards();
    }
}
