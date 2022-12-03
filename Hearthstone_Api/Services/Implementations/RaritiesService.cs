using Domain.Models;
using Hearthstone_Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hearthstone_Api.Services;

public class RaritiesService : RepositoryService<Domain.Models.Rarity, int>, IRaritiesService
{
    public RaritiesService(IMongoRepository<Rarity, int> repository) : base(repository)
    {
    }
}