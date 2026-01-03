using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Numbers;

public sealed class UInt64Column : RangeLimitedColumn<ulong>
{
    public override ValidationResult ValidateTypes(SchemaElement schemaElement) =>
        schemaElement.ValidateInteger(
            nameof(UInt64Column),
            bitWidth: 64,
            isSigned: false,
            compatibleConvertedType: ConvertedType.UINT_64,
            compatiblePhysicalType: Parquet.Meta.Type.INT64);
}
