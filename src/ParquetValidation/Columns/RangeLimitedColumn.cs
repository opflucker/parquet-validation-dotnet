using FluentValidation.Results;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns;

public class RangeLimitedColumn<T> : BaseColumn
    where T : struct, IComparable<T>
{
    public ValueRange<T>? Range { get; set; }

    public override ValidationResult ValidateData(object? data)
    {
        return data is null ? ValidateNullData()
            : data is not T typedData ? new([new ValidationFailure(Name, $"Value {data} is not of type {typeof(T).Name}.")])
            : ValidateTypedData(typedData);
    }

    public ValidationResult ValidateTypedData(T typedData)
    {
        if (Range != null && (Range.Min.HasValue || Range.Max.HasValue))
        {
            if (Range.Min.HasValue && typedData.CompareTo(Range.Min.Value) < 0)
            {
                return new([new ValidationFailure(Name, $"Value {typedData} is less than minimum allowed of {Range.Min.Value}.")]);
            }
            if (Range.Max.HasValue && typedData.CompareTo(Range.Max.Value) > 0)
            {
                return new([new ValidationFailure(Name, $"Value {typedData} exceeds maximum allowed of {Range.Max.Value}.")]);
            }
        }

        return new();
    }
}
