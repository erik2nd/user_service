using FluentMigrator;

namespace UsersService.DAL.Migrations;

[Migration(1, TransactionBehavior.None)]
public class InitSchema : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("guid").AsGuid().PrimaryKey()
            .WithColumn("login").AsString().NotNullable().Unique()
            .WithColumn("password").AsString().NotNullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("gender").AsInt32().NotNullable()
            .WithColumn("birthday").AsDate().Nullable()
            .WithColumn("admin").AsBoolean().NotNullable()
            .WithColumn("created_on").AsDateTime().NotNullable()
            .WithColumn("created_by").AsString().NotNullable()
            .WithColumn("modified_on").AsDateTime().Nullable()
            .WithColumn("modified_by").AsString().Nullable()
            .WithColumn("revoked_on").AsDateTime().Nullable()
            .WithColumn("revoked_by").AsString().Nullable();
        
        Execute.Sql(@"
            insert into users values (gen_random_uuid(), 'Admin',
                'Admin', 'Admin', 2, null, true, now(), 'Admin', now(), 'Admin', null, null);
            ");
    }

    public override void Down()
    {
        Delete.Table("users");
    }
}