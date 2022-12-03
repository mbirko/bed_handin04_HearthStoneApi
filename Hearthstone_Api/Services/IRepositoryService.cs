using Microsoft.AspNetCore.Mvc;

namespace Hearthstone_Api.Services;

public interface IRepositoryService<M, K> where M : Domain.Models.ModelBase<K>
{
    Task<ActionResult<M>> GetById(K id);
}