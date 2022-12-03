using Hearthstone_Api.Repositories;
using Hearthstone_Api.Repositories.Implementations;
using Hearthstone_Api.Services;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;

namespace Heartstone_Api_Tests;

[TestFixture]
public class SeedServiceTest
{
     private static MongoDbRunner _dbRunner;
     private static MongoClient _mongoClient;
     private static IMongoDatabase _mongoDatabase;
     private seedService<TestModel, TestDto, int> _uut;
     private IConvertService<TestModel, TestDto> _convertService;
     private IMongoRepository<TestModel, int> _repository;
     [SetUp]
     public void Setup()
     {
         var dbName = Guid.NewGuid().ToString();
         _dbRunner = MongoDbRunner.Start();
         _mongoClient = new MongoClient(_dbRunner.ConnectionString);
         _mongoDatabase = _mongoClient.GetDatabase(dbName);
         _repository = new MongoRepository<TestModel, int>(_mongoDatabase);
         _convertService = new ConvertService<TestModel, TestDto>();
         _uut = new seedService<TestModel, TestDto, int>(_repository, _convertService);

     }

     [TearDown]
     public void TearDown()
     {
         _dbRunner.Dispose();
     }



     [Test]
     public async Task SeedOneToDatabase()
     {
        var dto = TestDto.CreateTestDTO();

        await _uut.Seed(dto);
        var result = await _repository.GetAsync();
        
        Assert.That(dto.Data == result[0].Data);
     }
     [Test]
     public async Task SeedDataBase()
     {   
         // Arrange
         var dtos = new List<TestDto>();
         int i;
         for (i = 0; i < 10; ++i)
         {
            dtos.Add(TestDto.CreateTestDTO());
            dtos[i].Id = i;
         }
         // Act
         await _uut.Seed(dtos);
         // Assert
         var result= await _repository.GetAsync();
         Assert.That(await _repository.Count() == 10);
     }

     protected class TestModel : Domain.Models.ModelBase<int>
     {
         
         public string Data { get; set; } = null!;

         public static TestModel CreateTestModel()
         {
            return new TestModel()
            {
               Id = 1,
               Data = "This is a lot of data"
            };
         }
     }
     public class TestDto
     {
        public int Id { get; set; }
        public string Data { get; set; } = null!;

        public static TestDto CreateTestDTO()
        {
           return new TestDto()
           {
              Data = "This is a lot of data"
           };
        }

     }
}
