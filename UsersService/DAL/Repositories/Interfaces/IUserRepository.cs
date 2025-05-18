using UsersService.DAL.Entities;
using UsersService.PL.Models;

namespace UsersService.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    Task<bool> LoginExistsAsync(string login, CancellationToken token);
    Task<Guid> CreateAsync(UserEntity user, CancellationToken token);
    
    Task UpdateProfileAsync(string login, Dictionary<string, object> updatedFields, CancellationToken token);
    Task UpdatePasswordAsync(string login, string newPassword, DateTime modifiedOn, string modifiedBy,
        CancellationToken token);
    Task UpdateLoginAsync(string currentLogin, string newLogin, DateTime modifiedOn, string modifiedBy,
        CancellationToken token);
    
    Task<UserEntity?> GetByLoginAsync(string login, CancellationToken token);
    Task<IEnumerable<UserEntity>> GetAllActiveAsync(CancellationToken token);
    Task<UserEntity[]> GetOlderThanAgeAsync(int age, CancellationToken token);

    Task SoftDeleteAsync(string login, DateTime revokedOn, string revokedBy, CancellationToken token);
    Task HardDeleteAsync(string login, CancellationToken token);
    Task RestoreAsync(string login, CancellationToken token);
}