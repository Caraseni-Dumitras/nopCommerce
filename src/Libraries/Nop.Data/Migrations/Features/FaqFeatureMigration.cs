using FluentMigrator;

namespace Nop.Data.Migrations.Features;

[NopSchemaMigration("2023-06-13 10:00:00", "Create new table Faq")]
public class FaqFeatureMigration : MigrationBase
{
    public override void Up()
    {
        Create.Table("Faq")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey()
              .WithColumn("QuestionTitle").AsString(50).NotNullable()
              .WithColumn("QuestionDescription").AsString(500).NotNullable()
              .WithColumn("AnswerTitle").AsString(50).NotNullable()
              .WithColumn("AnswerDescription").AsString(500).NotNullable()
              .WithColumn("CategoryId").AsInt32().NotNullable();

        Create.ForeignKey("FK_Faq_Category")
              .FromTable("Faq")
              .ForeignColumn("CategoryId")
              .ToTable("Category")
              .PrimaryColumn("Id");
    }

    public override void Down()
    {
    }
}