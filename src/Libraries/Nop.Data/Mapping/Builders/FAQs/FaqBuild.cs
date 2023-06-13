using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.FAQs;

namespace Nop.Data.Mapping.Builders.FAQs;

public class FaqBuild : NopEntityBuilder<Faq>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.Id))).AsInt32().PrimaryKey()
            .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.QuestionTitle))).AsString(50).NotNullable()
            .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.QuestionDescription))).AsString(500).Nullable()
            .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.AnswerTitle))).AsString(50).Nullable()
            .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.AnswerDescription))).AsString(500).Nullable()
            .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Faq), nameof(Faq.CategoryId))).AsInt32().NotNullable();
    }
}