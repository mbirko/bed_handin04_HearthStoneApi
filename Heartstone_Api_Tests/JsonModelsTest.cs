using System.Text.Json;
using Hearthstone_Api.DTO;
using NUnit.Framework;

namespace Heartstone_Api_Tests;

[TestFixture]
public class JsonModelsTest
{
    [SetUp]
    public void SetUp()
    {
        
    }

    [TearDown]
    public void TearDown()
    {
        
    }

    [Test]
    public void GetCards()
    {
        var cardString =
            File.ReadAllText(
                "../../../../cards.json");
        List<CardJsonModel> cards = JsonSerializer.Deserialize<List<CardJsonModel>>(cardString);
       
        Assert.That(cards.Count == 4093);
    }

    [Test]
    public void GetMetaData()
    {
        var path = File.ReadAllText("../../../../metadata.json");
        MetaData metaData = JsonSerializer.Deserialize<MetaData>(path);
        
        Assert.That(metaData.Classes.Count > 0);
        Assert.That(metaData.Sets.Count > 0);
        Assert.That(metaData.Rarities.Count > 0);
        Assert.That(metaData.Types.Count > 0);
        
    }
}