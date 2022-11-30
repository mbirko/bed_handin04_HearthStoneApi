using firstMongoLib.data;
using firstMongoLib.Services;
using Hearthstone_Api.Data;
using Hearthstone_Api.Models;
using Microsoft.Extensions.Options;

namespace Hearthstone_Api.Services;

public class CardsService : GenericServices<Card>, ICardsServices
{
    public CardsService(IOptions<HearthstoneDbSettings> dbSettings): base(dbSettings)
    {
        Console.WriteLine(dbSettings);
    }
}