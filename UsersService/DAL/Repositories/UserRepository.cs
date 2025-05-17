using Dapper;
using Microsoft.Extensions.Options;
using UsersService.DAL.Entities;
using UsersService.DAL.Infrastructure;
using UsersService.DAL.Repositories.Interfaces;

namespace UsersService.DAL.Repositories;

public class UserRepository : PgRepository, IUserRepository
{
    public UserRepository(
        IOptions<DalOptions> dalSettings) : base(dalSettings.Value) { }
    
    public async Task<bool> LoginExistsAsync(string login, CancellationToken token)
    {
        const string sqlQuery = @"
            select exists (
                select 1
                from users
                where login = @Login
            );
            ";

        await using var connection = await GetConnection();
        var exists = await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sqlQuery,
                new { Login = login },
                cancellationToken: token));

        return exists;
    }

    public async Task<Guid> CreateAsync(UserEntity user, CancellationToken token)
    {
        const string sqlQuery = @"
            insert into users (
                guid, login, password, name, gender, birthday,
                admin, created_on, created_by
            ) values (
                @Guid, @Login, @Password, @Name, @Gender, @Birthday,
                @Admin, @CreatedOn, @CreatedBy
            );
        ";

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    user.Guid,
                    user.Login,
                    user.Password,
                    user.Name,
                    user.Gender,
                    user.Birthday,
                    user.Admin,
                    user.CreatedOn,
                    user.CreatedBy
                },
                cancellationToken: token));

        return user.Guid;
    }
    
    public async Task<UserEntity?> GetByLoginAsync(string login, CancellationToken token)
    {
        const string sqlQuery = @"
            select *
            from users
            where login = @Login
            limit 1;
        ";

        await using var connection = await GetConnection();
        return await connection.QueryFirstOrDefaultAsync<UserEntity>(
            new CommandDefinition(sqlQuery, new { Login = login }, cancellationToken: token));
    }
    
    public async Task<IEnumerable<UserEntity>> GetAllActiveAsync(CancellationToken token)
    {
        const string sqlQuery = @"
            select 
                *
            from users
            where revoked_on is null
            order by created_on;
            ";

        await using var connection = await GetConnection();
        var users = await connection.QueryAsync<UserEntity>(
            new CommandDefinition(sqlQuery, cancellationToken: token));
    
        return users.ToArray();
    }
    
    public async Task<UserEntity[]> GetOlderThanAgeAsync(int age, CancellationToken token)
    {
        const string sql = @"
            select * from users
            where birthday is not null 
              and revoked_on is null
              and extract(year from age(birthday)) > @Age";

        await using var connection = await GetConnection();
        var result = await connection.QueryAsync<UserEntity>(
            new CommandDefinition(sql, new { Age = age }, cancellationToken: token));

        return result.ToArray();
    }
    
    public async Task UpdateProfileAsync(string login, Dictionary<string, object> updatedFields, CancellationToken token)
    {
        var setClause = string.Join(", ", updatedFields.Keys.Select(k => $"{k} = @{k}"));
        var sql = $"update users set {setClause} where login = @login";

        var parameters = new DynamicParameters(updatedFields);
        parameters.Add("login", login);

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(sql, parameters, cancellationToken: token));
    }

}