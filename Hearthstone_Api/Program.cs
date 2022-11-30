using Domain.Models;
using Hearthstone_Api;
using Hearthstone_Api.Repositories;
using Hearthstone_Api.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;

// Dette gør du kan infer AppConfig fra appsettings
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
#else
                .AddJsonFile("activationsecrets/appsettings.secrets.json", optional: false, reloadOnChange: false)
                .AddJsonFile("appsecrets/appsettings.secrets.json", optional: false, reloadOnChange: false)

#endif
                .AddEnvironmentVariables()
                .Build();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

var config = configuration.Get<AppConfig>();

// MongoClient
var mongoClient = new MongoClient(config.HearthstoneDB.ConnectionString);
builder.Services.TryAddSingleton<IMongoClient>(provider => mongoClient);

// MongoDatabase
var audienceDb = mongoClient.GetDatabase(config.HearthstoneDB.DatabaseName);
builder.Services.TryAddSingleton(provider => audienceDb);

builder.Services.AddSingleton<ICardService, CardService>();
builder.Services.AddSingleton<IMongoRepository<Card, int>, CardsRepository>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
