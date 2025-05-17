using UsersService.DAL.Entities;
using UsersService.PL.Models;

namespace UsersService.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    Task<bool> LoginExistsAsync(string login, CancellationToken token);
    Task<Guid> CreateAsync(UserEntity user, CancellationToken token);
    Task UpdateProfileAsync(string login, Dictionary<string, object> updatedFields, CancellationToken token);
    
    Task<UserEntity?> GetByLoginAsync(string login, CancellationToken token);
    Task<IEnumerable<UserEntity>> GetAllActiveAsync(CancellationToken token);
    Task<UserEntity[]> GetOlderThanAgeAsync(int age, CancellationToken token);
    
}