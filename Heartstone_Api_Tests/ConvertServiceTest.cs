using Hearthstone_Api.Services;
using Heartstone_Api_Tests;
using NUnit.Framework;


namespace Heartstone_Api_Tests;

[TestFixture]
public class ConvertServiceTest
{
   public IConvertService<TestModel, TestDto> uut;
   [SetUp]
   public void SetUp()
   {
      uut = new ConvertService<TestModel, TestDto>();
   }

   [TearDown]
   public void TearDown()
   {
      
   }

   [Test]
   public void TestConvertToModel()
   {
      // arrange
      var modelTest = new TestModel();
      var dtoTest = TestDto.CreateTestDTO();

      // art
      modelTest = uut.ToModel(dtoTest);
      // assert
      Assert.That(modelTest.Data.Equals(dtoTest.Data));
   }

   [Test]
   public void TestConverToDTO()
   {
      // arrange
      var modelTest = TestModel.CreateTestModel();
      var dtoTest = new TestDto();

      // art
      dtoTest = uut.toDTO(modelTest);
      // assert
      Assert.That(dtoTest.Data.Equals(modelTest.Data));
      Assert.That(dtoTest.Id.Equals(modelTest.Id));
   }

   public class TestModel : Domain.Models.ModelBase<int>
   {
      public string _id { get; set; } = null!;
      public string Data { get; set; } = null!;

      public static TestModel CreateTestModel()
      {
         return new TestModel()
         {
            _id = "1234515",
            Id = 1,
            Data = "This is a lot of data"
         };
      }
   }

   public class TestDto
   {
      public int Id { get; set; }
      public string Data { get; set; } = null!;

      public static TestDto CreateTestDTO()
      {
         return new TestDto()
         {
            Data = "This is a lot of data"
         };
      }

   }
}

