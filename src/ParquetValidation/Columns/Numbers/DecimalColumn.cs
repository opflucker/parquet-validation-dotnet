using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Numbers;

public sealed class DecimalColumn : RangeLimitedColumn<decimal>
{
    public override ValidationResult ValidateTypes(SchemaElement schemaElement)
        => schemaElement.ValidateTypes(nameof(DecimalColumn),
            physicalType => physicalType == Parquet.Meta.Type.BYTE_ARRAY
                || physicalType == Parquet.Meta.Type.FIXED_LEN_BYTE_ARRAY
                || physicalType == Parquet.Meta.Type.INT64 && schemaElement.Precision <= 18
                || physicalType == Parquet.Meta.Type.INT32 && schemaElement.Precision <= 9,
            logicalType => logicalType.DECIMAL != null,
            convertedType => convertedType == ConvertedType.DECIMAL);
}
