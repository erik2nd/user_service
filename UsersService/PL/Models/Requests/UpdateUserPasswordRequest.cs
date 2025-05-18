namespace UsersService.PL.Models.Requests;

public record UpdateUserPasswordRequest(
    string Login,
    string NewPassword
);
