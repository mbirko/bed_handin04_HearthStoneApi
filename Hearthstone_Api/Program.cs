using System.Text.Json;
using Hearthstone_Api;
using Hearthstone_Api.Repositories;
using Hearthstone_Api.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;

// Configurations builder. 
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();


var config = configuration.Get<AppConfig>();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// MongoClient
var mongoClient = new MongoClient(config.HearthstoneDB.ConnectionString);
builder.Services.TryAddSingleton<IMongoClient>(_ => mongoClient);

// MongoDatabase
var db = mongoClient.GetDatabase(config.HearthstoneDB.DatabaseName);
builder.Services.TryAddSingleton(_ => db);


// Cards services and repo 
builder.Services.AddSingleton<ICardService, CardService>();
builder.Services.AddSingleton<IMongoRepository<Domain.Models.Card, int>, CardsRepository>();

// Classes services and repo 
builder.Services.AddSingleton<IClassService, ClassService>();
builder.Services.AddSingleton<IMongoRepository<Domain.Models.Class, int>, ClassesRepository>();
// Sets services and repo
builder.Services.AddSingleton<ISetsService, SetsService>();
builder.Services.AddSingleton<IMongoRepository<Domain.Models.Set, int>, SetsRepository>();

// Rarities services and repo 
builder.Services.AddSingleton<IRaritiesService, RaritiesService>();
builder.Services.AddSingleton<IMongoRepository<Domain.Models.Rarity, int>, RaritiesRepository>();

// Card(Types) services and repo 
builder.Services.AddSingleton<ITypesService, TypesService>();
builder.Services.AddSingleton<IMongoRepository<Domain.Models.CardType, int>, TypesRepository>();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var collections = db.ListCollectionNames().ToList();
var cardsRepo = new CardsRepository(db);
if (!collections.Contains(cardsRepo.ToString()))
{
    var seedService =
        new seedService<Domain.Models.Card, Hearthstone_Api.DTO.CardJsonModel, int>
            (cardsRepo, new ConvertService<Domain.Models.Card, Hearthstone_Api.DTO.CardJsonModel>());
    var cardsString = File.ReadAllText("./cards.json");
    var cardsList = JsonSerializer.Deserialize<List<Hearthstone_Api.DTO.CardJsonModel>>(cardsString);
    await seedService.Seed(cardsList!);
}


var path = File.ReadAllText("./metadata.json");
Hearthstone_Api.DTO.MetaData? metaData = JsonSerializer.Deserialize<Hearthstone_Api.DTO.MetaData>(path);

// Class seeding 
var classRepo = new ClassesRepository(db);
if (!collections.Contains(classRepo.ToString()))
{
    var classSeeder = 
        new seedService<
            Domain.Models.Class, 
            Hearthstone_Api.DTO.Class, 
            int>(classRepo, new ConvertService<
                Domain.Models.Class, 
                Hearthstone_Api.DTO.Class>());
    await classSeeder.Seed(metaData!.Classes);
}
// Sets seeding 
var setRepo = new SetsRepository(db);
if (!collections.Contains(setRepo.ToString()))
{
    var setSeeder = 
        new seedService<
            Domain.Models.Set, 
            Hearthstone_Api.DTO.Set, 
            int>(setRepo, new ConvertService<
                Domain.Models.Set, 
                Hearthstone_Api.DTO.Set>());
    await setSeeder.Seed(metaData!.Sets);
}


// Rarities seeding 
var raritiesRepo = new RaritiesRepository(db);
if (!collections.Contains(raritiesRepo.ToString()))
{
    var raritiesSeeder = 
        new seedService<
            Domain.Models.Rarity, 
            Hearthstone_Api.DTO.Rarity, 
            int>(raritiesRepo, new ConvertService<
                Domain.Models.Rarity, 
                Hearthstone_Api.DTO.Rarity>());
    await raritiesSeeder.Seed(metaData!.Rarities);
}


// Types seeding 

var typesRepo = new TypesRepository(db);
if (!collections.Contains(typesRepo.ToString()))
{
    var typesSeeder = 
        new seedService<
            Domain.Models.CardType, 
            Hearthstone_Api.DTO.CardType, 
            int>(typesRepo, new ConvertService<
                Domain.Models.CardType, 
                Hearthstone_Api.DTO.CardType>());
    await typesSeeder.Seed(metaData!.Types);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

