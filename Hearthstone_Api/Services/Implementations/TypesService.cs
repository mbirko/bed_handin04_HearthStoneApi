using Hearthstone_Api.Repositories;

namespace Hearthstone_Api.Services;

public class TypesService : RepositoryService<Domain.Models.CardType,int>, ITypesService
{
    public TypesService(IMongoRepository<Domain.Models.CardType, int> repository) : base(repository)
    {
    }
}