namespace UsersService.PL.Models.Responses;

public class UserResponse
{
    public Guid Guid { get; set; }
    public required string Login { get; set; } 
    public required string Name { get; set; }
    public int Gender { get; set; }
    public DateTime? Birthday { get; set; }
    public bool Admin { get; set; }
    public DateTime CreatedOn { get; set; }
}