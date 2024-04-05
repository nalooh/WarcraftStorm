using Microsoft.Extensions.DependencyInjection;

namespace WarcraftStorm.Network;

public static class ServiceCollectionWarcraftStormExtensions
{
    public static IServiceCollection AddWarcraftStormServer<TConnectionFactory>(this IServiceCollection services, int port)
    where TConnectionFactory : class, IConnectionFactory
    {
        services.Configure<ConnectionHandlerOptions>(options => 
        {
            options.Port = port;
        });
        services.AddSingleton<IConnectionFactory, TConnectionFactory>();
        services.AddHostedService<ConnectionHandler>();

        return services;
    }

}
