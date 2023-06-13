using FluentMigrator;
using Nop.Core.Domain.FAQs;
using Nop.Data.Mapping;

namespace Nop.Data.Migrations.Features;

[NopSchemaMigration("2023-06-13 10:00:00", "Create new table Faq")]
public class FaqFeatureMigration : MigrationBase
{
    public override void Up()
    {
        Create.Table("faq")
              .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.Id))).AsInt32().Identity().PrimaryKey()
              .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.QuestionTitle))).AsString(50).NotNullable()
              .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.QuestionDescription))).AsString(500).NotNullable()
              .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.AnswerTitle))).AsString(50).NotNullable()
              .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.AnswerDescription))).AsString(500).NotNullable()
              .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.CategoryId))).AsInt32().NotNullable();

        Create.ForeignKey("fk_faq_category")
              .FromTable("faq")
              .ForeignColumn((NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.CategoryId))))
              .ToTable("Category")
              .PrimaryColumn("Id");
    }

    public override void Down()
    {
    }
}