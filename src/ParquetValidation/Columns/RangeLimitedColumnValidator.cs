using FluentValidation;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns;

public class RangeLimitedColumnValidator<TColumn, TRange> : BaseColumnValidator<TColumn>
    where TColumn : RangeLimitedColumn<TRange>
    where TRange : struct, IComparable<TRange>
{
    public RangeLimitedColumnValidator()
    {
        RuleFor(column => column.Range!)
            .SetValidator(new ValueRangeValidator<TRange>())
            .When(column => column.Range != null);
    }
}