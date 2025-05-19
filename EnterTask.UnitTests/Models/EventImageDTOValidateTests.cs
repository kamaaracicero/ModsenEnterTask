using EnterTask.WebAPI.DTOs;
using EnterTask.WebAPI.Validation;
using FluentValidation.TestHelper;

namespace EnterTask.UnitTests.Models
{
    public class EventImageDTOValidateTests
    {
        private readonly EventImageDTOValidator _validator = new();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_EventId_Is_Invalid(int invalidId)
        {
            var model = new EventImageDTO { EventId = invalidId, Number = 0 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.EventId);
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(-1)]
        public void Should_Have_Error_When_Number_Is_Negative(int invalidNumber)
        {
            var model = new EventImageDTO { EventId = 1, Number = invalidNumber };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Model()
        {
            var model = new EventImageDTO { EventId = 1, Number = 0 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
