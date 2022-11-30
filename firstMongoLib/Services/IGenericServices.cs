using firstMongoLib.Models;

namespace firstMongoLib.Services;

public  interface  IGenericServices<ModelType>  where ModelType : ModelBase
{
    Task<List<ModelType>> GetAsync();
    Task<ModelType?> GetAsync(int id);
    Task CreateAsync(ModelType book);
    Task UpdateAsync(int id, ModelType book);
    Task DeleteAsync(int id);
}