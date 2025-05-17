namespace UsersService.PL.Models.Requests;

public class CreateUserRequest
{
    public required string Login { get; set; } 
    public required string Password { get; set; }
    public required string Name { get; set; }
    public int Gender { get; set; }
    public DateTime? Birthday { get; set; }
    public bool Admin { get; set; }
}
