using Hearthstone_Api.Repositories;

namespace Hearthstone_Api.Services.Implementations;

public class ClassService : RepositoryService<Domain.Models.Class, int>, IClassService
{
    public ClassService(IMongoRepository<Domain.Models.Class, int> repository) : base(repository)
    {
    }
}