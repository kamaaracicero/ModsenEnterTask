using EnterTask.WebAPI.DTOs;
using FluentValidation;

namespace EnterTask.WebAPI.Validation
{
    public class EventImageDTOValidator : AbstractValidator<EventImageDTO>
    {
        public EventImageDTOValidator()
        {
            RuleFor(e => e.EventId)
                .GreaterThan(0).WithMessage("Event id must be greater than 0");

            RuleFor(e => e.Number)
                .GreaterThanOrEqualTo(0).WithMessage("Number must be positive");
        }
    }
}
