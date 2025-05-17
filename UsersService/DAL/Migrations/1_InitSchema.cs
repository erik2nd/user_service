using FluentMigrator;

namespace UsersService.DAL.Migrations;

[Migration(1, TransactionBehavior.None)]
public class InitSchema : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("Guid").AsGuid().PrimaryKey()
            .WithColumn("Login").AsString().NotNullable().Unique()
            .WithColumn("Password").AsString().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Gender").AsInt32().NotNullable()
            .WithColumn("Birthday").AsDate().Nullable()
            .WithColumn("Admin").AsBoolean().NotNullable()
            .WithColumn("CreatedOn").AsDateTime().NotNullable()
            .WithColumn("CreatedBy").AsString().NotNullable()
            .WithColumn("ModifiedOn").AsDateTime().Nullable()
            .WithColumn("ModifiedBy").AsString().Nullable()
            .WithColumn("RevokedOn").AsDateTime().Nullable()
            .WithColumn("RevokedBy").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table("users");
    }
}