using Hearthstone_Api.Repositories;
namespace Hearthstone_Api.Services.Implementations;


public class SeedService<TM, TD, TK> 
    where TM : class, new()
    where TD : class, new()
{
    private readonly IMongoRepository<TM, TK> _repository;
    private readonly IConvertService<TM, TD> _convertService;
    public SeedService(IMongoRepository<TM, TK> repository, IConvertService<TM, TD> convertService)
    {
        _repository = repository;
        _convertService = convertService;
    }

    public async Task Seed(TD source)
    {
        var model = _convertService.ToModel(source);
        await _repository.CreateAsync(model);
    }

    public async Task Seed(List<TD> source)
    {
        foreach (var model in source)
        {
            await Seed(model);
        } 
    }
}
