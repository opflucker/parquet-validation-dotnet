using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace ParquetValidation.Columns.Strings.Formats;

[JsonConverter(typeof(BaseFormatDescriptorJsonConverter))]
public abstract class BaseFormatDescriptor
{
    public abstract ValidationResult ValidateData(string data, string columnName);
}
