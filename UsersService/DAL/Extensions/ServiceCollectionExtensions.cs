using UsersService.DAL.Infrastructure;
using UsersService.DAL.Repositories;
using UsersService.DAL.Repositories.Interfaces;

namespace UsersService.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(
        this IServiceCollection services)
    {
        AddPostgresRepositories(services);

        return services;
    }

    private static void AddPostgresRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static IServiceCollection AddDalInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot config)
    {
        //read config
        services.Configure<DalOptions>(config.GetSection(nameof(DalOptions)));

        //configure postrges types
        Postgres.MapCompositeTypes();

        //add migrations
        Postgres.AddMigrations(services);

        return services;
    }
}