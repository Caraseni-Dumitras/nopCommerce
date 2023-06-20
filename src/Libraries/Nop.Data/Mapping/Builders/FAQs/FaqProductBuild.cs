using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.FAQs;

namespace Nop.Data.Mapping.Builders.FAQs;

public class FaqProductBuild : NopEntityBuilder<FaqProductMapping>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(FaqProductMapping.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(FaqProductMapping.FaqId)).AsInt32().NotNullable()
            .WithColumn(nameof(FaqProductMapping.ProductId)).AsInt32().NotNullable();
    }
}