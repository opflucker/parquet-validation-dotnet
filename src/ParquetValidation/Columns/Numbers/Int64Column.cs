using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Numbers;

public sealed class Int64Column : RangeLimitedColumn<long>
{
    public override ValidationResult ValidateTypes(SchemaElement schemaElement) =>
        schemaElement.ValidateInteger(
            nameof(Int64Column),
            bitWidth: 64,
            isSigned: true,
            compatibleConvertedType: ConvertedType.INT_64,
            compatiblePhysicalType: Parquet.Meta.Type.INT64);
}
