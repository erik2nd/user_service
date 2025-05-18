namespace UsersService.PL.Models.Requests;

public record CreateUserRequest(
    string Login,
    string Password,
    string Name,
    int Gender,
    DateTime? Birthday,
    bool Admin
);

