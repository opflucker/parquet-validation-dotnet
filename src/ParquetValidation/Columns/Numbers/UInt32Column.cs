using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Numbers;

public sealed class UInt32Column : RangeLimitedColumn<uint>
{
    public override ValidationResult ValidateTypes(SchemaElement schemaElement) =>
        schemaElement.ValidateInteger(
            nameof(UInt32Column),
            bitWidth: 32,
            isSigned: false,
            compatibleConvertedType: ConvertedType.UINT_32,
            compatiblePhysicalType: Parquet.Meta.Type.INT32);
}
