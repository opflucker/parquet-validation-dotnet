using ParquetValidation.Commons;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ParquetValidation.Columns.Strings.Formats;

public sealed class BaseFormatDescriptorJsonConverter : JsonConverter<BaseFormatDescriptor>
{
    private static readonly Dictionary<string, Type> map = new(StringComparer.OrdinalIgnoreCase)
    {
        ["int32"] = typeof(Int32FormatDescriptor),
        ["int"] = typeof(Int32FormatDescriptor),
        ["datetime"] = typeof(DateTimeFormatDescriptor),
        ["date"] = typeof(DateOnlyFormatDescriptor),
        ["dateonly"] = typeof(DateOnlyFormatDescriptor),
        ["time"] = typeof(TimeOnlyFormatDescriptor),
        ["timeonly"] = typeof(TimeOnlyFormatDescriptor)
    };

    public override BaseFormatDescriptor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => JsonConverterExtensions.ReadCaseInsensitive<BaseFormatDescriptor>(ref reader, map, options);

    public override void Write(Utf8JsonWriter writer, BaseFormatDescriptor value, JsonSerializerOptions options)
        => JsonConverterExtensions.WriteNullable(writer, value, options);
}
