namespace UsersService.PL.Models.Responses;

public record UserByAgeResponse(
    string Name,
    int Gender,
    DateTime? Birthday,
    int Age
);
