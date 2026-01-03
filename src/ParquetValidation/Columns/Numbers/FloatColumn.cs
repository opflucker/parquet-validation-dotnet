using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Numbers;

public sealed class FloatColumn : RangeLimitedColumn<float>
{
    public override ValidationResult ValidateTypes(SchemaElement schemaElement)
        => schemaElement.ValidateFloat(
            nameof(FloatColumn),
            compatiblePhysicalType: Parquet.Meta.Type.FLOAT);
}
