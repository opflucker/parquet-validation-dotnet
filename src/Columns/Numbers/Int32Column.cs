using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Numbers;

public sealed class Int32Column : RangeLimitedColumn<int>
{
    public override ValidationResult ValidateTypes(SchemaElement schemaElement) =>
        schemaElement.ValidateInteger(
            nameof(Int32Column), 
            bitWidth: 32, 
            isSigned: true, 
            compatibleConvertedType: ConvertedType.INT_32, 
            compatiblePhysicalType: Parquet.Meta.Type.INT32);
}
