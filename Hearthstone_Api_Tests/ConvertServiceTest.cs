using Hearthstone_Api.Services;
using Hearthstone_Api.Services.Implementations;
// ReSharper disable RedundantAssignment

namespace Hearthstone_Api_Tests;

[TestFixture]
public class ConvertServiceTest
{
   private IConvertService<TestModel, TestDto> _uut = null!;
   [SetUp]
   public void SetUp()
   {
      _uut = new ConvertService<TestModel, TestDto>();
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
      var dtoTest = TestDto.CreateTestDto();

      // art
      modelTest = _uut.ToModel(dtoTest);
      // assert
      Assert.That(modelTest.Data.Equals(dtoTest.Data));
   }

   [Test]
   public void TestConvertToDto()
   {
      // arrange
      var modelTest = TestModel.CreateTestModel();
      var dtoTest = new TestDto();

      // art
      dtoTest = _uut.ToDto(modelTest);
      // assert
      Assert.That(dtoTest.Data.Equals(modelTest.Data));
      Assert.That(dtoTest.Id.Equals(modelTest.Id));
   }

   public class TestModel : Domain.Models.ModelBase<int>
   {
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

      public static TestDto CreateTestDto()
      {
         return new TestDto()
         {
            Data = "This is a lot of data"
         };
      }

   }
}

