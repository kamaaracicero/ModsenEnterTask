using EnterTask.WebAPI.DTOs;
using EnterTask.WebAPI.Validation;
using FluentValidation.TestHelper;

namespace EnterTask.UnitTests.Models
{
    public class PersonDTOValidateTests
    {
        private readonly PersonDTOValidator _validator = new();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_ParticipantId_Is_Invalid(int id)
        {
            var model = new PersonDTO { ParticipantId = id, Login = "user", Password = "Valid123!" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ParticipantId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("ab")]
        public void Should_Have_Error_When_Login_Is_Invalid(string login)
        {
            var model = new PersonDTO { ParticipantId = 1, Login = login, Password = "Valid123!" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Login);
        }

        [Fact]
        public void Should_Have_Error_When_Login_Too_Long()
        {
            var model = new PersonDTO { ParticipantId = 1, Login = new string('a', 201), Password = "Valid123!" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Login);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("short")]
        [InlineData("alllowercase1!")]
        [InlineData("ALLUPPERCASE1!")]
        [InlineData("NoDigits!")]
        [InlineData("NoSpecial123")]
        public void Should_Have_Error_When_Password_Invalid(string password)
        {
            var model = new PersonDTO { ParticipantId = 1, Login = "ValidUser", Password = password };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Model()
        {
            var model = new PersonDTO
            {
                ParticipantId = 1,
                Login = "ValidLogin",
                Password = "Valid123!"
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
