namespace UsersService.PL.Models.Requests;

public record DeleteUserRequest(
    string Login,        
    bool Hard
);
