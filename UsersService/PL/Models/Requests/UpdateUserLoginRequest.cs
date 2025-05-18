namespace UsersService.PL.Models.Requests;

public record UpdateUserLoginRequest(
    string CurrentLogin,
    string NewLogin
);
