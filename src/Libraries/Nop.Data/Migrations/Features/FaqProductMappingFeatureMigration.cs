using FluentMigrator;
using Nop.Core.Domain.FAQs;
using Nop.Data.Extensions;

namespace Nop.Data.Migrations.Features;

[NopSchemaMigration("2023-06-20 13:50:00", "Create new table Faq_Product_Mapping")]
public class FaqProductMappingFeatureMigration : MigrationBase
{
    public override void Up()
    {
        Create.TableFor<FaqProductMapping>();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}