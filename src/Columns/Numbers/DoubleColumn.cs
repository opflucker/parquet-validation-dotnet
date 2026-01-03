using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Numbers;

public sealed class DoubleColumn : RangeLimitedColumn<double>
{
    public override ValidationResult ValidateTypes(SchemaElement schemaElement)
        => schemaElement.ValidateFloat(
            nameof(FloatColumn),
            compatiblePhysicalType: Parquet.Meta.Type.DOUBLE);
}
