using Api.Requests;
using FluentValidation;

namespace Api.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(6).WithMessage("Name must be at least 6 characters.")
            .MaximumLength(60).WithMessage("Name must be between 6 and 60 characters.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Must be a valid email address.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(32).WithMessage("Password must be between 6 and 32 characters.");
    }
}