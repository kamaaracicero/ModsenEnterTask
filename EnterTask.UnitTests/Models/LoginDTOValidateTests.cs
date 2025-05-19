using EnterTask.WebAPI.DTOs;
using EnterTask.WebAPI.Validation;
using FluentValidation.TestHelper;

namespace EnterTask.UnitTests.Models
{
    public class LoginDTOValidateTests
    {
        private readonly LoginDTOValidator _validator = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Login_Is_Invalid(string login)
        {
            var model = new LoginDTO { Login = login, Password = "secret" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Login);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Password_Is_Invalid(string password)
        {
            var model = new LoginDTO { Login = "user", Password = password };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Model()
        {
            var model = new LoginDTO { Login = "user", Password = "secret" };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
