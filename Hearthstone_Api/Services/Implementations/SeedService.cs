using System.Text.Json;
using Hearthstone_Api.Repositories;
using Mapster;
namespace Hearthstone_Api.Services;


public class seedService<M, D, K> 
    where M : class, new()
    where D : class, new()
{
    private readonly IMongoRepository<M, K> _repository;
    private readonly IConvertService<M, D> _convertService;
    public seedService(IMongoRepository<M, K> repository, IConvertService<M, D> convertService)
    {
        _repository = repository;
        _convertService = convertService;
    }

    public async Task Seed(D source)
    {
        var model = _convertService.ToModel(source);
        await _repository.CreateAsync(model);
    }

    public async Task Seed(List<D> source)
    {
        foreach (var model in source)
        {
            await Seed(model);
        } 
    }
}
