namespace UsersService.DAL.Entities;

public record UserEntity
{
    public Guid Guid { get; init; }
    public required string Login { get; init; }
    public required string Password { get; init; } 
    public required string Name { get; set; }
    public int Gender { get; set; }
    public DateTime? Birthday { get; set; }
    public bool Admin { get; init; }
    public DateTime CreatedOn { get; init; }
    public required string CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? RevokedOn { get; init; }
    public string? RevokedBy { get; init; }
}
