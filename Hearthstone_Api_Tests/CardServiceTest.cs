using Hearthstone_Api.DTO;
using Hearthstone_Api.Repositories;
using Hearthstone_Api.Repositories.Implementations;
using Hearthstone_Api.Services;
using Mongo2Go;
using MongoDB.Driver;

namespace Hearthstone_Api_Tests
{
    public class CardServiceTests
    {
        private static MongoDbRunner? _dbRunner;
        private static MongoClient? _mongoClient;
        private static IMongoDatabase? _mongoDatabase;
        private IMongoRepository<Domain.Models.Card, int>? _repository;
        private IMongoRepository<Domain.Models.Class, int>? _classRepository; 
        private IMongoRepository<Domain.Models.CardType, int>? _typesRepository; 
        private IMongoRepository<Domain.Models.Set, int>? _setsRepository; 
        private IMongoRepository<Domain.Models.Rarity, int>? _raritiesRepository;
        private ICardService? _uut;
        
        [SetUp]
        public void Setup()
        {
            var dbName = Guid.NewGuid().ToString();
            _dbRunner = MongoDbRunner.Start();
            _mongoClient = new MongoClient(_dbRunner.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(dbName);
            _repository = new CardsRepository(_mongoDatabase);
            _classRepository = new ClassesRepository(_mongoDatabase);
            _typesRepository = new TypesRepository(_mongoDatabase);
            _setsRepository = new SetsRepository(_mongoDatabase);
            _raritiesRepository = new RaritiesRepository(_mongoDatabase);
            _uut = new CardService(
                _repository, 
                _classRepository, 
                _typesRepository, 
                _setsRepository,
                _raritiesRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _dbRunner!.Dispose();
        }

        [Test]
        public async Task TestNullFilter()
        {
            // Arrange

            var card = CreateCard();
            await _repository?.CreateAsync(card)!;
            // Act

            var filters = new CardFilters(null, null, null, null);
            var cards = await _uut?.GetCardsByFilterAsync(filters)!;

            // Assert
            if (cards.Value != null) Assert.That(cards.Value, Has.Count.EqualTo(1));
            else throw new NullReferenceException();
        }

        [Test]
        public async Task TestGetById()
        {
            List<ReturnCard> expected = await CreateTestData();

            var result = await _uut?.GetReturnCardsByFilterAsync(new CardFilters(null, null, null, null))!;

            if (result.Value != null)
            {
                Assert.That(result.Value.Count, Is.EqualTo(expected.Count));
                Assert.That(result.Value[1].Name, Is.EqualTo(expected[1].Name));
                Assert.That(result.Value[1].Set, Is.EqualTo(expected[1].Set));
                Assert.That(result.Value[1].Type, Is.EqualTo(expected[1].Type));
                Assert.That(result.Value[1].Class, Is.EqualTo(expected[1].Class));
                Assert.That(result.Value[1].Rarity, Is.EqualTo(expected[1].Rarity));
                Assert.That(result.Value[1].Artist, Is.EqualTo(expected[1].Artist));
                Assert.That(result.Value[1].Health, Is.EqualTo(expected[1].Health));
                Assert.That(result.Value[1].Attack, Is.EqualTo(expected[1].Attack));
                Assert.That(result.Value[1].FlavorText, Is.EqualTo(expected[1].FlavorText));
                Assert.That(result.Value[1].ManaCost, Is.EqualTo(expected[1].ManaCost));
            }
            else
            {
                throw new NullReferenceException();
            }
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

        private async Task<List<ReturnCard>> CreateTestData()
        {
            List<Domain.Models.Card> cards = new List<Domain.Models.Card>();
            List<Domain.Models.CardType> types = new List<Domain.Models.CardType>();
            List<Domain.Models.Set> sets = new List<Domain.Models.Set>();
            List<Domain.Models.Rarity> rarities = new List<Domain.Models.Rarity>();
            List<Domain.Models.Class> classes = new List<Domain.Models.Class>();
            List<ReturnCard> expected = new List<ReturnCard>();
            
            for (int i = 0; i < 3; ++i)
            {
                expected.Add(new ReturnCard()
                {
                    Artist = $"{i} artist",
                    Attack = i,
                    Health = i,
                    FlavorText = $"{i} flavor text",
                    ManaCost = i
                }); 
                
                cards.Add(new Domain.Models.Card()
                {
                    TypeId = i,
                    ClassId = i,
                    SetId = i,
                    RarityId = i,
                    Artist = $"{i} artist",
                    Attack = i,
                    Health = i,
                    FlavorText = $"{i} flavor text",
                    ManaCost = i
                });
                expected[i].Name = cards[i].Name = $"{i} " + cards[i].GetType();
                

                classes.Add(new Domain.Models.Class() { Id = i });
                expected[i].Class = classes[i].Name = $"{i} " + classes[i].GetType() ;
                
                types.Add(new Domain.Models.CardType() { Id = i, });
                expected[i].Type = types[i].Name = $"{i} " + types[i].GetType() ;
                
                sets.Add(new Domain.Models.Set() { Id = i, });
                expected[i].Set = sets[i].Name = $"{i} " + sets[i].GetType() ;
                
                rarities.Add(new Domain.Models.Rarity() { Id = i, });
                expected[i].Rarity = rarities[i].Name = $"{i} " + rarities[i].GetType() ;
            }

            await _repository?.CreateManyAsync(cards)!;
            await _classRepository?.CreateManyAsync(classes)!;
            await _setsRepository?.CreateManyAsync(sets)!;
            await _raritiesRepository?.CreateManyAsync(rarities)!;
            await _typesRepository?.CreateManyAsync(types)!;
            return (expected);
        }

    }
}