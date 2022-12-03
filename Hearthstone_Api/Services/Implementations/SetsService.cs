using Hearthstone_Api.Repositories;

namespace Hearthstone_Api.Services;

public class SetsService : RepositoryService<Domain.Models.Set, int>, ISetsService
{
    public SetsService(IMongoRepository<Domain.Models.Set, int> repository) : base(repository)
    {
    }
}