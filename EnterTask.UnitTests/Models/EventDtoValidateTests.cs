using EnterTask.WebAPI.DTOs;
using EnterTask.WebAPI.Validation;
using FluentValidation.TestHelper;

namespace EnterTask.UnitTests.Models
{
    public class EventDtoValidateTests
    {
        private readonly EventDTOValidator _validator = new EventDTOValidator();

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Have_Error_When_Name_Is_NullOrEmpty(string name)
        {
            var model = new EventDTO { Name = name };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Name);
        }

        [Theory]
        [InlineData(301)]
        public void Should_Have_Error_When_Name_Too_Long(int length)
        {
            var model = new EventDTO { Name = new string('a', length) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Name);
        }

        [Theory]
        [InlineData(2001)]
        public void Should_Have_Error_When_Description_Too_Long(int length)
        {
            var model = new EventDTO { Description = new string('b', length) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Description);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_Have_Error_When_MaxPeopleCount_NotPositive(int count)
        {
            var model = new EventDTO { MaxPeopleCount = count };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.MaxPeopleCount);
        }

        [Theory]
        [InlineData(-1)]
        public void Should_Have_Error_When_Start_In_Past(int minutesOffset)
        {
            var model = new EventDTO { Start = DateTime.Now.AddMinutes(minutesOffset) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Start);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Have_Error_When_Place_Is_NullOrEmpty(string place)
        {
            var model = new EventDTO { Place = place };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Place);
        }

        [Theory]
        [InlineData(151)]
        public void Should_Have_Error_When_Place_Too_Long(int length)
        {
            var model = new EventDTO { Place = new string('c', length) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Place);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Have_Error_When_Category_Is_NullOrEmpty(string category)
        {
            var model = new EventDTO { Category = category };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Category);
        }

        [Theory]
        [InlineData(101)]
        public void Should_Have_Error_When_Category_Too_Long(int length)
        {
            var model = new EventDTO { Category = new string('d', length) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Category);
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Model_Is_Valid()
        {
            var model = new EventDTO
            {
                Name = "Test Event",
                Description = "Valid description",
                Start = DateTime.Now.AddHours(1),
                Place = "Venue",
                Category = "Music",
                MaxPeopleCount = 50
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
