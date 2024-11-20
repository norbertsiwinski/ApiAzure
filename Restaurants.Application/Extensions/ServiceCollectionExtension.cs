using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        var appAssembly = typeof(ServiceCollectionExtension).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly((appAssembly)));
        services.AddAutoMapper(appAssembly);

        services.AddValidatorsFromAssembly(appAssembly)
            .AddFluentValidationAutoValidation();
    }
}