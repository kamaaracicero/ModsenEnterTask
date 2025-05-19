using EnterTask.WebAPI.DTOs;
using FluentValidation;

namespace EnterTask.WebAPI.Validation
{
    public class PersonDTOValidator : AbstractValidator<PersonDTO>
    {
        public PersonDTOValidator()
        {
            RuleFor(p => p.ParticipantId)
                .GreaterThan(0).WithMessage("Participant id must be greater than 0");

            RuleFor(p => p.Login)
                .NotNull().WithMessage("Login required")
                .NotEmpty().WithMessage("Login must not be empty")
                .MinimumLength(3).WithMessage("Login must be at least 3 characters long")
                .MaximumLength(200).WithMessage("Login must not exceed 200 characters in length");

            RuleFor(p => p.Password)
                .NotNull().WithMessage("Password required")
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches("[A-Z]").WithMessage("The password must contain at least one capital letter")
                .Matches("[a-z]").WithMessage("The password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("The password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("The password must contain at least one special character");
        }
    }
}
