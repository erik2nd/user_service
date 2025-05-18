using Dapper;
using FluentMigrator.Runner;
using UsersService.DAL.Entities;
using Microsoft.Extensions.Options;
using Npgsql;
using Npgsql.NameTranslation;

namespace UsersService.DAL.Infrastructure;

public static class Postgres
{
    private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

    /// <summary>
    ///     Map DAL models to composite types (enables UNNEST)
    /// </summary>
    public static void MapCompositeTypes()
    {
        var mapper = NpgsqlConnection.GlobalTypeMapper;
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        mapper.MapComposite<UserEntity>("users", Translator);
    }

    /// <summary>
    ///     Add migration infrastructure
    /// </summary>
    public static void AddMigrations(IServiceCollection services)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(s =>
                {
                    var cfg = s.GetRequiredService<IOptions<DalOptions>>();
                    return cfg.Value.PostgresConnectionString;
                })
                .ScanIn(typeof(Postgres).Assembly).For.Migrations()
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }
}