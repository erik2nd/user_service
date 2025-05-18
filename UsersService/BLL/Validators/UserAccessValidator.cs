using UsersService.BLL.Exceptions;
using UsersService.BLL.Validators.Interfaces;
using UsersService.DAL.Repositories.Interfaces;

namespace UsersService.BLL.Validators;

public class UserAccessValidator : IUserAccessValidator
{
    private readonly IUserRepository _userRepository;

    public UserAccessValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task EnsureAdminAsync(string login, CancellationToken token)
    {
        var user = await _userRepository.GetByLoginAsync(login, token)
                   ?? throw new NotFoundException();

        if (!user.Admin || user.RevokedOn != null)
            throw new ForbiddenException();
    }

    public async Task EnsureAdminOrSelfActiveAsync(string requesterLogin, string targetLogin, CancellationToken token)
    {
        var requester = await _userRepository.GetByLoginAsync(requesterLogin, token)
                        ?? throw new NotFoundException();

        if (requester.RevokedOn != null)
            throw new ForbiddenException();

        if (!requester.Admin && requester.Login != targetLogin)
            throw new ForbiddenException();
    }
}
