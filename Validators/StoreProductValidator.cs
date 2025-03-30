using Api.Requests;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Api.Validators;

public class StoreProductValidator : AbstractValidator<StoreProductRequest>
{
    public StoreProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(5).WithMessage("Name must be at least 5 characters");

        RuleFor(product => product.Description)
            .MaximumLength(10000).WithMessage("Description must be between 1 and 10000 characters");

        RuleFor(product => product.Price)
            .NotNull()
            .WithMessage("Price is required")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than or equal 0");

        RuleFor(product => product.Thumb)
            .Must(thumb => string.IsNullOrEmpty(thumb) || thumb.StartsWith("http://") || thumb.StartsWith("https://"))
            .WithMessage("Thumb must be null or a valid URL starting with 'http://' or 'https://'.");

        RuleForEach(product => product.Images)
            .Must(image => image.StartsWith("http://") || image.StartsWith("https://"))
            .WithMessage("Each image must be a valid URL starting with 'http://' or 'https://'.");

        RuleFor(product => product.Stock)
            .NotNull()
            .WithMessage("Price is required")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than or equal 0");
    }
}