using GitWrap.Application.Interfaces;
using GitWrap.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GitWrap.Application;

public static class Configuration
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblies(typeof(Configuration).Assembly);
        });

        return services.RegisterServices();
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGitHostingServiceRepositoryResolver<>), 
            typeof(GitHostingServiceRepositoryResolver<>));

        return services;
    }
}