namespace UsersService.PL.Models.Responses;

public record UserByLoginAndPasswordResponse(
    string Name,
    int Gender,
    DateTime? Birthday
);
