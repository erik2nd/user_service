using UsersService.PL.Models;
using UsersService.PL.Models.Requests;
using UsersService.PL.Models.Responses;

namespace UsersService.BLL.Interfaces;

public interface IUserService
{
    Task<Guid> CreateUserAsync(CreateUserRequest request, string createdByLogin,  CancellationToken token);
    Task UpdateUserProfileAsync(string requesterLogin, UpdateUserProfileRequest request,
        CancellationToken token);
    
    Task<IEnumerable<UserResponse>> GetAllActiveUsersAsync(string requesterLogin, CancellationToken token);
    Task<UserByLoginResponse> GetUserByLoginAsync(string requesterLogin, string targetLogin, CancellationToken token);
    Task<UserByLoginAndPasswordResponse> GetUserByLoginAndPassword(string requesterLogin, string targetLogin, string password, CancellationToken token);
    Task<UserByAgeResponse[]> GetUsersOlderThanAgeAsync(string requesterLogin, int age, CancellationToken token);

}