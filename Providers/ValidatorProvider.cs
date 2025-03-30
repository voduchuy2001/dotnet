using Api.Requests;
using Api.Validators;
using FluentValidation;

namespace Api.Providers;

public static class ValidatorProvider
{
    public static IServiceCollection AddValidatorProvider(this IServiceCollection services)
    {
        services.AddScoped<IValidator<StoreProductRequest>, StoreProductValidator>();
        services.AddScoped<IValidator<UpdateProductRequest>, UpdateProductValidator>();
        services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
        services.AddScoped<IValidator<RegisterRequest>, RegisterValidator>();
        return services;
    }
}