using EnterTask.WebAPI.DTOs;
using FluentValidation;

namespace EnterTask.WebAPI.Validation
{
    public class ParticipantDTOValidator : AbstractValidator<ParticipantDTO>
    {
        public ParticipantDTOValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(e => e.Surname)
                .NotEmpty().WithMessage("Surname is required")
                .MaximumLength(100);

            RuleFor(e => e.DateOfBirth)
                .Must(date => date < DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Date of birth must be in the past");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be valid")
                .MaximumLength(500);
        }
    }
}
