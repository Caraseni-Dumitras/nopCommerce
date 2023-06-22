using FluentMigrator;
using Nop.Core.Domain.FAQs;
using Nop.Data.Extensions;
using Nop.Data.Mapping;

namespace Nop.Data.Migrations.Features;

[NopSchemaMigration("2023-06-22 11:00:00", "Create tables for FAQ")]
public class FaqFeatureMigration : MigrationBase
{
    public override void Up()
    {
        Create.TableFor<Faq>();
        Create.TableFor<FaqProductMapping>();
        Create.TableFor<FaqCategoryMapping>();

        Create.ForeignKey("FK_Faq_Category_Mapping_FaqId_Faq_Id")
              .FromTable("Faq_Category_Mapping")
              .ForeignColumn("FaqId")
              .ToTable(NameCompatibilityManager.GetTableName(typeof(Faq)))
              .PrimaryColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.Id)));

        Create.ForeignKey("FK_Faq_Category_Mapping_CategoryId_Category_Id")
              .FromTable("Faq_Category_Mapping")
              .ForeignColumn("CategoryId")
              .ToTable("Category")
              .PrimaryColumn("Id");

        Create.ForeignKey("FK_Faq_Product_Mapping_FaqId_Faq_Id")
              .FromTable("Faq_Product_Mapping")
              .ForeignColumn("FaqId")
              .ToTable(NameCompatibilityManager.GetTableName(typeof(Faq)))
              .PrimaryColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.Id)));

        Create.ForeignKey("FK_Faq_Product_Mapping_ProductId_Product_Id")
              .FromTable("Faq_Product_Mapping")
              .ForeignColumn("ProductId")
              .ToTable("Product")
              .PrimaryColumn("Id");
    }

    public override void Down()
    {
    }
}