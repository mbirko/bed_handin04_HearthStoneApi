using Microsoft.AspNetCore.Mvc;

namespace Hearthstone_Api.Services;

public interface IRepositoryService<TM, in TK> where TM : Domain.Models.ModelBase<TK>
{
    Task<ActionResult<TM>> GetById(TK id);
}