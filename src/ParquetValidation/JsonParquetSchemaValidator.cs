using FluentValidation;
using ParquetValidation.Columns;
using ParquetValidation.Columns.Booleans;
using ParquetValidation.Columns.Dates;
using ParquetValidation.Columns.Numbers;
using ParquetValidation.Columns.Strings;

namespace ParquetValidation;

public sealed class  JsonParquetSchemaValidator : AbstractValidator<JsonParquetSchema>
{
    public JsonParquetSchemaValidator()
    {
        RuleFor(ts => ts.Columns)
            .NotEmpty()
            .WithMessage($"{nameof(JsonParquetSchema.Columns)} must have at least one element.");
        RuleFor(ts => ts.Columns)
            .Must(columns => columns.Select(c => c.Name).Distinct().Count() == columns.Count)
            .WithMessage($"{nameof(JsonParquetSchema.Columns)} element names must be unique.");
        RuleFor(ts => ts.Columns)
            .ForEach(column =>
            {
                column.SetInheritanceValidator(v => 
                {
                    v.Add(new StringColumnValidator());
                    v.Add(new RangeLimitedColumnValidator<Int32Column, int>());
                    v.Add(new RangeLimitedColumnValidator<Int64Column, long>());
                    v.Add(new RangeLimitedColumnValidator<UInt32Column, uint>());
                    v.Add(new RangeLimitedColumnValidator<UInt64Column, ulong>());
                    v.Add(new RangeLimitedColumnValidator<FloatColumn, float>());
                    v.Add(new RangeLimitedColumnValidator<DoubleColumn, double>());
                    v.Add(new RangeLimitedColumnValidator<DecimalColumn, decimal>());
                    v.Add(new RangeLimitedColumnValidator<DateColumn, DateOnly>());
                    v.Add(new RangeLimitedColumnValidator<DateTimeColumn, DateTime>());
                    v.Add(new BaseColumnValidator<BooleanColumn>());
                });
            });
    }
}