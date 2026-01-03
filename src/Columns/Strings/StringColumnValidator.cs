using FluentValidation;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Strings;

public class StringColumnValidator : BaseColumnValidator<StringColumn>
{
    public StringColumnValidator()
    {
        RuleFor(sc => sc.Length!)
            .SetValidator(new ValueRangeValidator<uint>())
            .When(sc => sc.Length != null);
    }
}