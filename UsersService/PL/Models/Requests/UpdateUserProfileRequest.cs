namespace UsersService.PL.Models.Requests;

public record UpdateUserProfileRequest(
    string Login,
    string? NewName,
    int? NewGender,
    DateTime? NewBirthday
);