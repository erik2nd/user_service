namespace UsersService.PL.Models.Responses;

public record UserResponse(
    Guid Guid,
    string Login,
    string Name,
    int Gender,
    DateTime? Birthday,
    bool Admin,
    DateTime CreatedOn
);
