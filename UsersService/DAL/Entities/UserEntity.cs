namespace UsersService.DAL.Entities;

public record UserEntity
{
    public Guid Guid { get; init; }
    public required string Login { get; init; }
    public required string Password { get; init; } 
    public required string Name { get; init; }
    public int Gender { get; init; }
    public DateTime? Birthday { get; init; }
    public bool Admin { get; init; }
    public DateTime CreatedOn { get; init; }
    public required string CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public string? ModifiedBy { get; init; }
    public DateTime? RevokedOn { get; init; }
    public string? RevokedBy { get; init; }
}
