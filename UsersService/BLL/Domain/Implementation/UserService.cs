using UsersService.BLL.Exceptions;
using UsersService.BLL.Interfaces;
using UsersService.BLL.Validators.Interfaces;
using UsersService.DAL.Entities;
using UsersService.DAL.Repositories.Interfaces;
using UsersService.PL.Models.Requests;
using UsersService.PL.Models.Responses;

namespace UsersService.BLL.Domain.Implementation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserAccessValidator _accessValidator;

    public UserService(IUserRepository userRepository, IUserAccessValidator accessValidator)
    {
        _userRepository = userRepository;
        _accessValidator = accessValidator;
    }
    
    public async Task<Guid> CreateUserAsync(CreateUserRequest request, string requesterLogin, CancellationToken token)
    {
        await _accessValidator.EnsureAdminAsync(requesterLogin, token);
        
        var existing = await _userRepository.GetByLoginAsync(request.Login, token);
        if (existing != null)
            throw new ExistingUserExecption();

        var user = new UserEntity
        {
            Guid = Guid.NewGuid(),
            Login = request.Login,
            Password = request.Password,
            Name = request.Name,
            Gender = request.Gender,
            Birthday = request.Birthday,
            Admin = request.Admin,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = requesterLogin
        };

        await _userRepository.CreateAsync(user, token);
        
        return user.Guid;
    }
    
    public async Task<IEnumerable<UserResponse>> GetAllActiveUsersAsync(string requesterLogin, CancellationToken token)
    {
        await _accessValidator.EnsureAdminAsync(requesterLogin, token);

        var users = await _userRepository.GetAllActiveAsync(token);

        return users.Select(user => new UserResponse(
            user.Guid,
            user.Login,
            user.Name,
            user.Gender,
            user.Birthday,
            user.Admin,
            user.CreatedOn
        )).ToArray();

    }
    
    public async Task<UserByLoginResponse> GetUserByLoginAsync(string requesterLogin, string targetLogin, CancellationToken token)
    {
        await _accessValidator.EnsureAdminAsync(requesterLogin, token);
        
        var targetUser = await _userRepository.GetByLoginAsync(targetLogin, token)
                         ?? throw new NotFoundException();

        return new UserByLoginResponse(
            targetUser.Name,
            targetUser.Gender,
            targetUser.Birthday,
            targetUser.RevokedOn == null
        );
    }
    
    public async Task<UserByLoginAndPasswordResponse> GetUserByLoginAndPassword(string requesterLogin, string targetLogin, string password, CancellationToken token)
    {
        if (targetLogin != requesterLogin)
            throw new ForbiddenException();
        
        var user = await _userRepository.GetByLoginAsync(targetLogin, token)
                   ?? throw new NotFoundException();

        if (user.RevokedOn != null)
            throw new ForbiddenException();

        if (user.Password != password)
            throw new ForbiddenException();

        return new UserByLoginAndPasswordResponse(user.Name, user.Gender, user.Birthday);
    }
    
    public async Task<UserByAgeResponse[]> GetUsersOlderThanAgeAsync(string requesterLogin, int age, CancellationToken token)
    {
        await _accessValidator.EnsureAdminAsync(requesterLogin, token);
        
        var users = await _userRepository.GetOlderThanAgeAsync(age, token);

        return users.Select(u => new UserByAgeResponse(
            u.Name,
            u.Gender,
            u.Birthday,
            (u.Birthday is { } b ? GetAge(b) : 0)
        )).ToArray();
    }

    private int GetAge(DateTime birthday)
    {
        var now = DateTime.UtcNow;
        var age = now.Year - birthday.Year;
        if (birthday.Date > now.AddYears(-age)) age--;
        return age;
    }
    
    public async Task UpdateUserProfileAsync(string requesterLogin, UpdateUserProfileRequest request, CancellationToken token)
    {
        await _accessValidator.EnsureAdminOrSelfActiveAsync(requesterLogin, request.Login, token);
       
        var target = await _userRepository.GetByLoginAsync(request.Login, token)
                     ?? throw new NotFoundException();
        
        var updatedFields = new Dictionary<string, object>();

        if (request.NewName != null)
        {
            target.Name = request.NewName;
            updatedFields["name"] = request.NewName;
        }

        if (request.NewGender.HasValue)
        {
            target.Gender = request.NewGender.Value;
            updatedFields["gender"] = request.NewGender.Value;
        }

        if (request.NewBirthday.HasValue)
        {
            target.Birthday = request.NewBirthday;
            updatedFields["birthday"] = request.NewBirthday;
        }

        if (updatedFields.Count == 0)
            return; 

        target.ModifiedOn = DateTime.UtcNow;
        target.ModifiedBy = requesterLogin;

        updatedFields["modified_on"] = target.ModifiedOn;
        updatedFields["modified_by"] = target.ModifiedBy;

        await _userRepository.UpdateProfileAsync(target.Login, updatedFields, token);
    }
    
    public async Task UpdateUserPasswordAsync(string requesterLogin, UpdateUserPasswordRequest request, CancellationToken token)
    {
        await _accessValidator.EnsureAdminOrSelfActiveAsync(requesterLogin, request.Login, token);

        var target = await _userRepository.GetByLoginAsync(request.Login, token)
                     ?? throw new NotFoundException();

        target.Password = request.NewPassword;
        target.ModifiedOn = DateTime.UtcNow;
        target.ModifiedBy = requesterLogin;

        await _userRepository.UpdatePasswordAsync(target.Login, target.Password, (DateTime)target.ModifiedOn, target.ModifiedBy, token);
    }

    public async Task UpdateUserLoginAsync(string requesterLogin, UpdateUserLoginRequest request, CancellationToken token)
    {
        var requester = await _userRepository.GetByLoginAsync(requesterLogin, token)
                        ?? throw new NotFoundException();

        var target = await _userRepository.GetByLoginAsync(request.CurrentLogin, token)
                     ?? throw new NotFoundException();

        if (requester.Login != target.Login)
        {
            if (!requester.Admin || requester.RevokedOn != null)
                throw new ForbiddenException();
        }
        else
        {
            if (target.RevokedOn != null)
                throw new ForbiddenException();
        }

        var existing = await _userRepository.GetByLoginAsync(request.NewLogin, token);
        if (existing != null)
            throw new ExistingUserExecption();

        await _userRepository.UpdateLoginAsync(
            request.CurrentLogin,
            request.NewLogin,
            DateTime.UtcNow,
            requesterLogin,
            token);
    }

    public async Task DeleteUserAsync(string requesterLogin, DeleteUserRequest request, CancellationToken token)
    {
        await _accessValidator.EnsureAdminAsync(requesterLogin, token);

        var user = await _userRepository.GetByLoginAsync(request.Login, token)
                   ?? throw new NotFoundException();

        if (request.Hard)
        {
            await _userRepository.HardDeleteAsync(request.Login, token);
        }
        else
        {
            await _userRepository.SoftDeleteAsync(
                request.Login,
                DateTime.UtcNow,
                requesterLogin,
                token);
        }
    }
    
    public async Task RestoreUserAsync(string requesterLogin, RestoreUserRequest request, CancellationToken token)
    {
        await _accessValidator.EnsureAdminAsync(requesterLogin, token);

        var user = await _userRepository.GetByLoginAsync(request.Login, token)
                   ?? throw new NotFoundException();

        await _userRepository.RestoreAsync(request.Login, token);
    }
}