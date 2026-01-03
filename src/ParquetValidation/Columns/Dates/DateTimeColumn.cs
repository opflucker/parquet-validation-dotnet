using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Dates;

public sealed class DateTimeColumn : RangeLimitedColumn<DateTime>
{
    public override ValidationResult ValidateTypes(SchemaElement schemaElement)
        => schemaElement.ValidateTypes(nameof(DateTimeColumn),
            physicalType => physicalType == Parquet.Meta.Type.INT64,
            logicalType => logicalType.TIMESTAMP != null,
            convertedType => 
                convertedType == ConvertedType.TIMESTAMP_MILLIS
                || convertedType == ConvertedType.TIMESTAMP_MICROS);
}