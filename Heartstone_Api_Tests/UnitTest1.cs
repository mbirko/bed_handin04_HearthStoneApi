using Hearthstone_Api.Repositories;
using Hearthstone_Api.Services;
using Mongo2Go;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Heartstone_Api_Tests
{
    public class CardServiceTests
    {
        private static MongoDbRunner _dbRunner;
        private static MongoClient _mongoClient;
        private static IMongoDatabase _mongoDatabase;
        private static string _collectionName = "cards";

        [SetUp]
        public void Setup()
        {
            var dbName = Guid.NewGuid().ToString();
            _dbRunner = MongoDbRunner.Start();
            _mongoClient = new MongoClient(_dbRunner.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(dbName);
        }

        [TearDown]
        public void TearDown()
        {
            _dbRunner.Dispose();
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            var mongoRepository = new CardsRepository(_mongoDatabase);

            var card = CreateCard();
            await InsertToMongo(card);

            // Act
            var uut = new CardService(mongoRepository);

            var filters = new CardFilters(null, null, null, null);
            var cards = await uut.GetCardsByFilter(filters);

            // Assert
            Assert.That(1, Is.EqualTo(cards.Value.Count));
        }

        private Domain.Models.Card CreateCard()
        {
            return new Domain.Models.Card()
            {
                Artist = "Mathias",
                ClassId = 1,
                SetId = 1,
                RarityId = 1,
            };
        }

        private async Task InsertToMongo(Domain.Models.Card card)
        {
            var collection = _mongoDatabase.GetCollection<Domain.Models.Card>(_collectionName);
            await collection.InsertOneAsync(card);
        }

        private async Task GetFromMongo(Expression<Func<Domain.Models.Card, bool>> filter)
        {
            var collection = _mongoDatabase.GetCollection<Domain.Models.Card>(_collectionName);
            await collection.Find(filter).ToListAsync();
        }
    }
}