using Hearthstone_Api.Repositories;

namespace Hearthstone_Api.Services.Implementations;

public class RaritiesService : RepositoryService<Domain.Models.Rarity, int>, IRaritiesService
{
    public RaritiesService(IMongoRepository<Domain.Models.Rarity, int> repository) : base(repository)
    {
    }
}