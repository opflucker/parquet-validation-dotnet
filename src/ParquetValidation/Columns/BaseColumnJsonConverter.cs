using ParquetValidation.Columns.Booleans;
using ParquetValidation.Columns.Dates;
using ParquetValidation.Columns.Numbers;
using ParquetValidation.Columns.Strings;
using ParquetValidation.Commons;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ParquetValidation.Columns;

public sealed class BaseColumnJsonConverter : JsonConverter<BaseColumn>
{
    private static readonly Dictionary<string, Type> map = new(StringComparer.OrdinalIgnoreCase)
    {
        ["string"] = typeof(StringColumn),
        ["str"] = typeof(StringColumn),
        ["int32"] = typeof(Int32Column),
        ["int"] = typeof(Int32Column),
        ["int64"] = typeof(Int64Column),
        ["long"] = typeof(Int64Column),
        ["uint32"] = typeof(UInt32Column),
        ["uint64"] = typeof(UInt64Column),
        ["float"] = typeof(FloatColumn),
        ["double"] = typeof(DoubleColumn),
        ["decimal"] = typeof(DecimalColumn),
        ["date"] = typeof(DateColumn),
        ["datetime"] = typeof(DateTimeColumn),
        ["boolean"] = typeof(BooleanColumn),
        ["bool"] = typeof(BooleanColumn)
    };

    public override BaseColumn? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => JsonConverterExtensions.ReadCaseInsensitive<BaseColumn>(ref reader, map, options);

    public override void Write(Utf8JsonWriter writer, BaseColumn value, JsonSerializerOptions options)
        => JsonConverterExtensions.WriteNullable(writer, value, options);
}
