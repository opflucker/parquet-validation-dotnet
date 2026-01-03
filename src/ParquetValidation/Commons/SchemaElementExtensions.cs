using FluentValidation.Results;
using Parquet.Meta;

namespace ParquetValidation.Commons;

public static class SchemaElementExtensions
{
    public static ValidationResult ValidateInteger(this SchemaElement schemaElement, string columnTypeName, sbyte bitWidth, bool isSigned, ConvertedType compatibleConvertedType, Parquet.Meta.Type compatiblePhysicalType)
        => schemaElement.ValidateTypes(columnTypeName,
            physicalType => physicalType == compatiblePhysicalType,
            logicalType => logicalType.INTEGER != null
                && logicalType.INTEGER.BitWidth <= bitWidth 
                && logicalType.INTEGER.IsSigned == isSigned,
            convertedType => convertedType == compatibleConvertedType);

    public static ValidationResult ValidateFloat(this SchemaElement schemaElement, string columnTypeName, Parquet.Meta.Type compatiblePhysicalType)
        => schemaElement.ValidateTypes(columnTypeName, 
            physicalType => physicalType == compatiblePhysicalType,
            logicalType => logicalType.FLOAT16 != null);

    public static ValidationResult ValidateTypes(
        this SchemaElement schemaElement, 
        string columnTypeName,
        Predicate<Parquet.Meta.Type> physicalTypeCondition,
        Predicate<LogicalType>? logicalTypeCondition = null,
        Predicate<ConvertedType>? convertedTypeCondition = null)
    {
        return schemaElement.LogicalType != null && logicalTypeCondition != null && !logicalTypeCondition(schemaElement.LogicalType)
                ? new([new ValidationFailure(schemaElement.Name, $"SchemaElement LogicalType is not compatible with {columnTypeName}.")])
            : schemaElement.ConvertedType != null && convertedTypeCondition != null && !convertedTypeCondition(schemaElement.ConvertedType.Value)
                ? new([new ValidationFailure(schemaElement.Name, $"SchemaElement ConvertedType is not compatible with {columnTypeName}.")])
            : schemaElement.Type == null
                ? new([new ValidationFailure(schemaElement.Name, $"SchemaElement physical type is required for {columnTypeName}.")])
            : !physicalTypeCondition(schemaElement.Type.Value)
                ? new([new ValidationFailure(schemaElement.Name, $"SchemaElement physical type is not compatible with {columnTypeName}.")])
            : new();
    }
}