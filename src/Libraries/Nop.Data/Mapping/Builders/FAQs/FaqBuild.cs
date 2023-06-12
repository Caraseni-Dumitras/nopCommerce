using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.FAQs;

namespace Nop.Data.Mapping.Builders.FAQs;

public class FaqBuild : NopEntityBuilder<Faq>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(Faq.QuestionTitle)).AsString(50).NotNullable()
            .WithColumn(nameof(Faq.QuestionDescription)).AsString(500).Nullable()
            .WithColumn(nameof(Faq.AnswerTitle)).AsString(50).Nullable()
            .WithColumn(nameof(Faq.AnswerDescription)).AsString(500).Nullable()
            .WithColumn(nameof(Faq.CategoryId)).AsInt32().NotNullable();
    }
}