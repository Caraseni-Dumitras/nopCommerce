using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.FAQs;

namespace Nop.Data.Mapping.Builders.FAQs;

public class FaqCategoryBuild : NopEntityBuilder<FaqCategoryMapping>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(FaqCategoryMapping.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(FaqCategoryMapping.FaqId)).AsInt32().NotNullable()
            .WithColumn(nameof(FaqCategoryMapping.CategoryId)).AsInt32().NotNullable();
    }
}