using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Columns.Strings.Formats;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Strings;

public sealed class StringColumn : BaseColumn
{
    public ValueRange<uint>? Length { get; set; }
    public BaseFormatDescriptor? Format { get; set; }

    public override ValidationResult ValidateTypes(SchemaElement schemaElement)
        => schemaElement.ValidateTypes(
            nameof(StringColumn),
            physicalType => physicalType == Parquet.Meta.Type.BYTE_ARRAY,
            logicalType => logicalType.STRING != null,
            convertedType => convertedType == ConvertedType.UTF8);

    public override ValidationResult ValidateData(object? data)
    {
        if (data is not string stringData)
        {
            return new([new ValidationFailure(Name, "Data is not of type string.")]);
        }

        if (Length != null && (Length.Min.HasValue || Length.Max.HasValue))
        {
            if (Length.Min.HasValue && stringData.Length < Length.Min.Value)
            {
                return new([new ValidationFailure(Name, $"String length {stringData.Length} is less than minimum allowed length of {Length.Min.Value}.")]);
            }
            if (Length.Max.HasValue && stringData.Length > Length.Max.Value)
            {
                return new([new ValidationFailure(Name, $"String length {stringData.Length} exceeds maximum allowed length of {Length.Max.Value}.")]);
            }
        }

        if (Format != null)
        {
            var formatValidationResult = Format.ValidateData(stringData, Name);
            if (!formatValidationResult.IsValid)
            {
                return formatValidationResult;
            }
        }

        return new();
    }
}
