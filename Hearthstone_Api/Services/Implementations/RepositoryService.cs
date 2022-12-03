using Domain.Models;
using Hearthstone_Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Hearthstone_Api.Services;

public class RepositoryService<M, K> : IRepositoryService<M, K> where M : Domain.Models.ModelBase<K>
{
    protected readonly IMongoRepository<M, K> _repository;

    public RepositoryService(IMongoRepository<M, K> repository)
    {
        _repository = repository;
    }
    
    public async Task<ActionResult<M>> GetById(K id)
    {
        var list = await _repository.GetAsync(Builders<M>.Filter.Where(x => x.Id.Equals(id)));
        if (list.Count != 1) return new NotFoundResult();
        return list[0];
    }
}