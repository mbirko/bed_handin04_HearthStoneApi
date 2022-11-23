// See https://aka.ms/new-console-template for more information

using firstMongoLib.data;
using firstMongoLib.Models;
using firstMongoLib.Services;

Console.WriteLine("Hello, World!");

var mongo = new BookServices(new MongoDbSettings());

await mongo.CreateAsync(new BookModel()
{
    Author = "jens",
    BookName = "bogen",
    Category = "categori",
    Price = Decimal.One,

});
var temp = await mongo.GetAsync();

await mongo.UpdateAsync(temp[0].id, new BookModel()
{
    Author = "Klaus",
    BookName = "Kluases Book",
    Category = "category",
    Price = decimal.One,

});
temp = await mongo.GetAsync();

Console.WriteLine("heh");

