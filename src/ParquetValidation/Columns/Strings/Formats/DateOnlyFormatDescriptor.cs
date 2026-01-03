using FluentValidation.Results;

namespace ParquetValidation.Columns.Strings.Formats;

public sealed class DateOnlyFormatDescriptor : BaseFormatDescriptor
{
    public string Pattern { get; set; } = "yyyy-MM-dd";

    public override ValidationResult ValidateData(string data, string columnName)
        => DateOnly.TryParseExact(data, Pattern, null, System.Globalization.DateTimeStyles.None, out var _)
            ? new()
            : new([new ValidationFailure(columnName, $"'{data}' is not a valid DateOnly with pattern '{Pattern}'")]);
}
