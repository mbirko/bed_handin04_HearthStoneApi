using Hearthstone_Api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hearthstone_Api.Services;
    public interface ICardService : IRepositoryService<Domain.Models.Card, int>
    {
        Task<ActionResult<List<Domain.Models.Card>>> GetCardsByFilterAsync(CardFilters filters);
        Task<ActionResult<List<ReturnCard>>> GetReturnCardsByFilterAsync(CardFilters filters);
    }
