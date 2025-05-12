using EnterTask.WebAPI.DTOs;
using EnterTask.WebAPI.Validation;
using FluentValidation.TestHelper;

namespace EnterTask.UnitTests.Models
{
    public class ParticipantDTOValidateTests
    {
        private readonly ParticipantDTOValidator _validator = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Name_Is_Null_Or_Empty(string name)
        {
            var model = new ParticipantDTO { Name = name, Surname = "Test", Email = "test@test.com", DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20)) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Too_Long()
        {
            var model = new ParticipantDTO { Name = new string('a', 101), Surname = "Test", Email = "test@test.com", DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20)) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Surname_Is_Null_Or_Empty(string surname)
        {
            var model = new ParticipantDTO { Name = "Test", Surname = surname, Email = "test@test.com", DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20)) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.Surname);
        }

        [Fact]
        public void Should_Have_Error_When_Surname_Too_Long()
        {
            var model = new ParticipantDTO { Name = "Test", Surname = new string('b', 101), Email = "test@test.com", DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20)) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.Surname);
        }

        [Fact]
        public void Should_Have_Error_When_DateOfBirth_Is_In_Future()
        {
            var model = new ParticipantDTO { Name = "Test", Surname = "Test", Email = "test@test.com", DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddDays(1)) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.DateOfBirth);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid-email")]
        public void Should_Have_Error_When_Email_Is_Invalid(string email)
        {
            var model = new ParticipantDTO { Name = "Test", Surname = "Test", Email = email, DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20)) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Too_Long()
        {
            var email = new string('a', 495) + "@test.com";
            var model = new ParticipantDTO { Name = "Test", Surname = "Test", Email = email, DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20)) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.Email);
        }

        [Fact]
        public void Should_Not_Have_Errors_For_Valid_Model()
        {
            var model = new ParticipantDTO
            {
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-25))
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
