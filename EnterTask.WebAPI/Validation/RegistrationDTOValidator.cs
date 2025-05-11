using EnterTask.WebAPI.DTOs;
using FluentValidation;

namespace EnterTask.WebAPI.Validation
{
    public class RegistrationDTOValidator : AbstractValidator<RegistrationDTO>
    {
        public RegistrationDTOValidator()
        {
            RuleFor(r => r.EventId)
                .GreaterThan(0).WithMessage("Event id must be greater than 0");

            RuleFor(r => r.ParticipantId)
                .GreaterThan(0).WithMessage("Participant id must be greater than 0");
        }
    }
}
