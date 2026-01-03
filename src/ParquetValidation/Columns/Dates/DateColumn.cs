using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Dates;

public sealed class DateColumn : RangeLimitedColumn<DateOnly>
{
    public override ValidationResult ValidateTypes(SchemaElement schemaElement)
        => schemaElement.ValidateTypes(nameof(DateColumn),
            physicalType => physicalType == Parquet.Meta.Type.INT32,
            logicalType => logicalType.DATE != null,
            convertedType => convertedType == ConvertedType.DATE);

    public override ValidationResult ValidateData(object? data)
    {
        // Parquet.Net reads columns of type DATE as DateTime with time component set to midnight
        return data is null ? ValidateNullData()
            : data is not DateTime typedData ? new([new ValidationFailure(Name, $"Value {data} is not of type {nameof(DateTime)}.")])
            : ValidateTypedData(DateOnly.FromDateTime(typedData));
    }
}
