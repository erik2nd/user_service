namespace UsersService.PL.Models.Responses;

public record UserByLoginResponse(
    string Name,
    int Gender,
    DateTime? Birthday,
    bool IsActive
);