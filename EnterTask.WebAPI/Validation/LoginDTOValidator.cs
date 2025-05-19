using EnterTask.WebAPI.DTOs;
using FluentValidation;

namespace EnterTask.WebAPI.Validation
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(p => p.Login)
                .NotNull().WithMessage("Login required")
                .NotEmpty().WithMessage("Login must not be empty");

            RuleFor(p => p.Password)
                .NotNull().WithMessage("Password required")
                .NotEmpty().WithMessage("Password must not be empty");
        }
    }
}
