using FluentValidation;

namespace ParquetValidation.Commons;

public sealed class ValueRangeValidator<T> : AbstractValidator<ValueRange<T>>
    where T : struct, IComparable<T>
{
    public ValueRangeValidator()
    {
        RuleFor(vr => vr)
            .Must(vr => !vr.Min.HasValue || !vr.Max.HasValue || vr.Min.Value.CompareTo(vr.Max.Value) <= 0)
            .WithMessage("Min must be less than or equal to Max.");
    }
}