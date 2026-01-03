using FluentValidation.Results;

namespace ParquetValidation.Columns.Strings.Formats;

public sealed class TimeOnlyFormatDescriptor : BaseFormatDescriptor
{
    public string Pattern { get; set; } = "HH:mm:ss.FFF";

    public override ValidationResult ValidateData(string data, string columnName)
        => TimeOnly.TryParseExact(data, Pattern, null, System.Globalization.DateTimeStyles.None, out var _)
            ? new()
            : new([new ValidationFailure(columnName, $"'{data}' is not a valid TimeOnly with pattern '{Pattern}'")]);
}
