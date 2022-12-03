using System.Diagnostics;
using System.Text.Json;
using Hearthstone_Api.DTO;

namespace Hearthstone_Api_Tests;

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
        List<CardJsonModel>? cards = JsonSerializer.Deserialize<List<CardJsonModel>>(cardString);
       
        Assert.That(cards != null && cards.Count == 4093);
    }

    [Test]
    public void GetMetaData()
    {
        var path = File.ReadAllText("../../../../metadata.json");
        MetaData? metaData = JsonSerializer.Deserialize<MetaData>(path);

        Debug.Assert(metaData != null, nameof(metaData) + " != null");
        Debug.Assert(metaData.Classes != null, "metaData.Classes != null");
        Assert.That(metaData.Classes.Count, Is.GreaterThan(0));
        Debug.Assert(metaData.Sets != null, "metaData.Sets != null");
        Assert.That(metaData.Sets.Count, Is.GreaterThan(0));
        Debug.Assert(metaData.Rarities != null, "metaData.Rarities != null");
        Assert.That(metaData.Rarities.Count > 0);
        Debug.Assert(metaData.Types != null, "metaData.Types != null");
        Assert.That(metaData.Types.Count > 0);
        
    }
}