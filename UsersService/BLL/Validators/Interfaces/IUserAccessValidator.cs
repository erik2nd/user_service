namespace UsersService.BLL.Validators.Interfaces;

public interface IUserAccessValidator
{
    Task EnsureAdminAsync(string login, CancellationToken token);
    Task EnsureActiveAsync(string login, CancellationToken token);
    Task EnsureAdminOrSelfActiveAsync(string requesterLogin, string targetLogin, CancellationToken token);
}