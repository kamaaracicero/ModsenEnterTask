using EnterTask.WebAPI.DTOs;
using FluentValidation;

namespace EnterTask.WebAPI.Validation
{
    public class EventDTOValidator : AbstractValidator<EventDTO>
    {
        public EventDTOValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(300);

            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(2000);

            RuleFor(e => e.Start)
                .GreaterThan(DateTime.Now).WithMessage("Date must be in the future");

            RuleFor(e => e.Place)
                .NotEmpty().WithMessage("Place is required")
                .MaximumLength(150);

            RuleFor(e => e.Category)
                .NotEmpty().WithMessage("Category is required")
                .MaximumLength(100);

            RuleFor(e => e.MaxPeopleCount)
                .GreaterThan(0).WithMessage("Max people count must be greater than 0");
        }
    }
}
