using ParquetValidation;
using System.Text.Json;

public class JsonParquetSchemaReader
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        AllowOutOfOrderMetadataProperties = true
    };

    public static JsonParquetSchema ReadFrom(string json)
    {
        return JsonSerializer.Deserialize<JsonParquetSchema>(json, Options)
               ?? throw new InvalidOperationException("Failed to deserialize schema from JSON.");
    }
}
