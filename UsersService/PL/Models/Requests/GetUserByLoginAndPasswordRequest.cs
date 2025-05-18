namespace UsersService.PL.Models.Requests;

public record GetUserByLoginAndPasswordRequest(
    string Login,
    string Password
);
