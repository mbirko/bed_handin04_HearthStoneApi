using Hearthstone_Api.Data;
using Hearthstone_Api.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddLogging();
var temp = builder.Configuration.GetSection("HearthstoneDB");

builder.Services.Configure<HearthstoneDbSettings>(temp);

builder.Services.AddSingleton<ICardsServices, CardsService>();

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
