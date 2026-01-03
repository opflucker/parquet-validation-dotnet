using System.Text.Json;

namespace ParquetValidation.Commons;

public static class JsonConverterExtensions
{
    const string DiscriminatorPropertyName = "type";

    /// <summary>
    /// Read implementation that looks up the discriminator property case-insensitively
    /// and deserializes into the appropriate derived type.
    /// </summary>
    public static T? ReadCaseInsensitive<T>(ref Utf8JsonReader reader, Dictionary<string, Type> map, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        // Find discriminator case-insensitively
        string? discriminator = null;
        foreach (var prop in root.EnumerateObject())
        {
            if (string.Equals(prop.Name, DiscriminatorPropertyName, StringComparison.OrdinalIgnoreCase))
            {
                if (prop.Value.ValueKind == JsonValueKind.String)
                    discriminator = prop.Value.GetString();
                else
                    discriminator = prop.Value.ToString();
                break;
            }
        }

        if (string.IsNullOrWhiteSpace(discriminator))
            throw new JsonException($"Missing or empty '{DiscriminatorPropertyName}' discriminator for {typeof(T).Name}.");

        if (!map.TryGetValue(discriminator.Trim(), out var targetType))
        {
            // try direct match against declared derived type names if not found in aliases
            foreach (var t in map.Values)
            {
                if (string.Equals(t.Name, discriminator, StringComparison.OrdinalIgnoreCase))
                {
                    targetType = t;
                    break;
                }
            }
        }

        if (targetType == null)
            throw new JsonException($"Unknown descriptor type '{discriminator}'.");

        var raw = root.GetRawText();
        var result = (T?)JsonSerializer.Deserialize(raw, targetType, options);
        if (result == null)
            throw new JsonException($"Failed to deserialize {typeof(T).Name} as {targetType.FullName}.");

        return result;
    }

    public static void WriteNullable<T>(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        where T : class
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}