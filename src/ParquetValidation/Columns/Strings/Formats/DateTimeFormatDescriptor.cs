using FluentValidation.Results;

namespace ParquetValidation.Columns.Strings.Formats;

public sealed class DateTimeFormatDescriptor : BaseFormatDescriptor
{
    public string Pattern { get; set; } = "o";

    public override ValidationResult ValidateData(string data, string columnName)
        => DateTime.TryParseExact(data, Pattern ?? "o", null, System.Globalization.DateTimeStyles.None, out var _)
            ? new()
            : new([new ValidationFailure(columnName, $"'{data}' is not a valid DateTime with pattern '{Pattern}'")]);
}
