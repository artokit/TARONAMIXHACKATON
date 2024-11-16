using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(0)]
public class M0000_InitialMigration : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\"");
        
        Create.Table("users")
            .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
            .WithColumn("email").AsString().NotNullable().Unique()
            .WithColumn("hashed_password").AsString().NotNullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("surname").AsString().NotNullable()
            .WithColumn("patronymic").AsString().Nullable()
            .WithColumn("role").AsString().NotNullable();

        Create.Table("companies")
            .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("director_id").AsInt64().NotNullable()
            .WithColumn("image_id").AsGuid().Nullable();

        Create.Table("companies_users")
            .WithColumn("company_id").AsInt32().NotNullable()
            .WithColumn("user_id").AsInt32().NotNullable();
        
        Create.Table("workers")
            .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
            .WithColumn("email").AsString().NotNullable().Unique()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("surname").AsString().NotNullable()
            .WithColumn("patronymic").AsString().Nullable()
            .WithColumn("is_leader").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("is_candidate").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("birthday").AsDate().NotNullable()
            .WithColumn("unit_id").AsInt32().NotNullable()
            .WithColumn("company_id").AsInt32().NotNullable();

        Create.Table("reports")
            .WithColumn("id").AsInt32()
            .WithColumn("candidate_id").AsInt32()
            .WithColumn("created_at").AsDateTime();
        
        Create.Table("results")
            .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
            .WithColumn("report_id").AsInt32().PrimaryKey().NotNullable().Identity()
            .WithColumn("worker_id").AsInt32()
            .WithColumn("first_candidate_id").AsInt32().NotNullable()
            .WithColumn("second_candidate_id").AsInt32().NotNullable()
            .WithColumn("test").AsInt32()
            .WithColumn("skills").AsInt32();

        Create.Table("tags")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("worker_id").AsInt32()
            .WithColumn("type").AsString().NotNullable()
            .WithColumn("name").AsString();

        Create.Table("units")
            .WithColumn("id").AsInt32().Identity().PrimaryKey().
            WithColumn("company_id").AsInt32().NotNullable()
            .WithColumn("name").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("users");
        Delete.Table("companies");
        Delete.Table("workers");
        Delete.Table("companies_users");
        Delete.Table("reports");
        Delete.Table("results");
        Delete.Table("tags");
        Delete.Table("units");
    }
}