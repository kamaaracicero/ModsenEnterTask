using EnterTask.WebAPI.DTOs;
using EnterTask.WebAPI.Validation;
using FluentValidation.TestHelper;

namespace EnterTask.UnitTests.Models
{
    public class RegistrationDTOValidateTests
    {
        private readonly RegistrationDTOValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_EventId_Is_Less_Than_Or_Equal_Zero()
        {
            var model = new RegistrationDTO { EventId = 0, ParticipantId = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.EventId);
        }

        [Fact]
        public void Should_Have_Error_When_ParticipantId_Is_Less_Than_Or_Equal_Zero()
        {
            var model = new RegistrationDTO { EventId = 1, ParticipantId = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.ParticipantId);
        }

        [Fact]
        public void Should_Not_Have_Error_When_EventId_And_ParticipantId_Are_Valid()
        {
            var model = new RegistrationDTO { EventId = 1, ParticipantId = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
