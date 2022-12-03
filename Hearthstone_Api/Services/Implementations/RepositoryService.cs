using Hearthstone_Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Hearthstone_Api.Services.Implementations;

public class RepositoryService<TM, TK> : IRepositoryService<TM, TK> where TM : Domain.Models.ModelBase<TK>
{
    protected readonly IMongoRepository<TM, TK> Repository;

    public RepositoryService(IMongoRepository<TM, TK> repository)
    {
        Repository = repository;
    }
    
    public async Task<ActionResult<TM>> GetById(TK id)
    {
        var list = await Repository.GetAsync(Builders<TM>.Filter.Where(x => x.Id!.Equals(id)));
        if (list.Count != 1) return new NotFoundResult();
        return list[0];
    }
}