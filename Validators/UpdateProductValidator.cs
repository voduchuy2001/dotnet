using Api.Requests;
using FluentValidation;

namespace Api.Validators;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(product => product.Name)
           .NotEmpty().WithMessage("Name is required")
           .MinimumLength(5).WithMessage("Name must be at least 5 characters");

        RuleFor(product => product.Description)
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(10).WithMessage("Description must be at least 10 characters");

        RuleFor(product => product.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than or equal 0");
    }
}