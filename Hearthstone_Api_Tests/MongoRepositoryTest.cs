using Hearthstone_Api.Repositories;
using Hearthstone_Api.Repositories.Implementations;
using Mongo2Go;
using MongoDB.Driver;

namespace Hearthstone_Api_Tests;

[TestFixture]
public class MongoRepositoryTest
{
    
     private static MongoDbRunner _dbRunner = null!;
     private static MongoClient _mongoClient = null!;
     private static IMongoDatabase _mongoDatabase = null!;
     private IMongoRepository<TestModel, int> _uut = null!;
     [SetUp]
     public void Setup()
     {
         var dbName = Guid.NewGuid().ToString();
         _dbRunner = MongoDbRunner.Start();
         _mongoClient = new MongoClient(_dbRunner.ConnectionString);
         _mongoDatabase = _mongoClient.GetDatabase(dbName);
         _uut = new MongoRepository<TestModel, int>(_mongoDatabase);

     }

     [TearDown]
     public void TearDown()
     {
         _dbRunner.Dispose();
     }

     [Test]
     public async Task TestCreateAndGetAsync()
     {
         // arragne 
         var model = TestModel.CreateTestModel();
         // act
         await _uut.CreateAsync(model);
         var modelCreated = await _uut.GetAsync();
         // assert
         Assert.That(model.Data == modelCreated[0].Data);
         Assert.That(model.Id == modelCreated[0].Id);
         Console.WriteLine("_id: " + modelCreated[0]._id);
     }
     [Test]
     public async Task TestCreateManyAndGetAsync()
     {
         // arrange 
         var model = new List<TestModel>();
         for (var i = 0; i < 10; ++i)
         {
             model.Add(TestModel.CreateTestModel());
             model[i].Id = i;
         }
            
         // act
         await _uut.CreateManyAsync(model);
         var modelCreated = await _uut.GetAsync();
         // assert
         Assert.That(model.Count == modelCreated.Count);
     }
     [Test]
     public async Task TestCountAsync()
     {
         // arrange 
         var model = new List<TestModel>();
         for (var i = 0; i < 10; ++i)
         {
             model.Add(TestModel.CreateTestModel());
             model[i].Id = i;
         }
            
         // act
         await _uut.CreateManyAsync(model);
         var modelCreated = await _uut.Count();
         // assert
         Assert.That(model.Count == modelCreated);
     }

     [Test]
     public async Task TestCountNoCollection()
     {
         const long expected = 0;
         var result = await _uut.Count();
        
        Assert.That(result, Is.EqualTo(expected));
     }
     
     [Test]
     public async Task TestNameOfCollectionBase()
    {
        await _uut.CreateAsync(TestModel.CreateTestModel());
         var cardRepo = new CardsRepository(_mongoDatabase);
         await cardRepo.CreateAsync(new Domain.Models.Card());
         var collectionNames = await (await _mongoDatabase.ListCollectionNamesAsync()).ToListAsync();
         var result = collectionNames.Contains(_uut.ToString());
         var result1 = collectionNames.Contains(cardRepo.ToString());
         
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(true));
            Assert.That(result1, Is.EqualTo(true));
        });
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

        public static TestDto CreateTestDto()
        {
           return new TestDto()
           {
              Data = "This is a lot of data"
           };
        }

     }
}