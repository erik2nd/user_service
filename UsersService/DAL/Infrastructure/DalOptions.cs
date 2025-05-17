namespace UsersService.DAL.Infrastructure;

public record DalOptions
{
    public required string PostgresConnectionString { get; init; } = string.Empty;
}