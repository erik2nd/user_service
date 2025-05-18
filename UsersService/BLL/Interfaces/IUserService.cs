using UsersService.PL.Models;
using UsersService.PL.Models.Requests;
using UsersService.PL.Models.Responses;

namespace UsersService.BLL.Interfaces;

public interface IUserService
{
    Task<Guid> CreateUserAsync(CreateUserRequest request, string createdByLogin,  CancellationToken token);
    
    Task UpdateUserProfileAsync(string requesterLogin, UpdateUserProfileRequest request,
        CancellationToken token);
    Task UpdateUserPasswordAsync(string requesterLogin, UpdateUserPasswordRequest request,
        CancellationToken token);
    Task UpdateUserLoginAsync(string requesterLogin, UpdateUserLoginRequest request,
        CancellationToken token);
    
    Task<IEnumerable<UserResponse>> GetAllActiveUsersAsync(string requesterLogin, CancellationToken token);
    Task<UserByLoginResponse> GetUserByLoginAsync(string requesterLogin, string targetLogin, CancellationToken token);
    Task<UserByLoginAndPasswordResponse> GetUserByLoginAndPassword(string requesterLogin, string targetLogin, string password, CancellationToken token);
    Task<UserByAgeResponse[]> GetUsersOlderThanAgeAsync(string requesterLogin, int age, CancellationToken token);

    Task DeleteUserAsync(string requesterLogin, DeleteUserRequest request, CancellationToken token);
    Task RestoreUserAsync(string requesterLogin, RestoreUserRequest request, CancellationToken token);
}