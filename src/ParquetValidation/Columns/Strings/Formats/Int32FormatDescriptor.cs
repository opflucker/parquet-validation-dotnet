using FluentValidation.Results;

namespace ParquetValidation.Columns.Strings.Formats;

public sealed class Int32FormatDescriptor : BaseFormatDescriptor
{
    public override ValidationResult ValidateData(string data, string columnName)
        => int.TryParse(data, out var _)
        ? new()
        : new([new ValidationFailure(columnName, $"'{data}' is not a valid Int32")]);
}
