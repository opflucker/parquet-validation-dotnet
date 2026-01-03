using ParquetValidation.Columns.Strings;
using ParquetValidation.Columns.Strings.Formats;

namespace ParquetValidator.Tests.Columns;

public class StringColumnTests
{
    [Fact]
    public void Deserialize_StringColumn_WithLength_Returns_StringColumn()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""myString"",
                    ""type"": ""String"",
                    ""length"": { ""min"": 0, ""max"": 20 },
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<StringColumn>(schema.Columns[0]);
        Assert.Equal("myString", col.Name);
        Assert.False(col.Required);
        Assert.NotNull(col.Length);
        Assert.Equal(0u, col.Length.Min);
        Assert.Equal(20u, col.Length.Max);
    }

    [Fact]
    public void Deserialize_StringColumn_WithDateTimeFormat_Returns_DateTimeFormatDescriptor()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""dateTimeAsString"",
                    ""type"": ""String"",
                    ""length"": { ""min"": 0, ""max"": 50 },
                    ""format"": { ""type"": ""DateTime"", ""pattern"": ""MM-dd-yyyy hh:mm:ss"" },
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<StringColumn>(schema.Columns[0]);
        Assert.NotNull(col.Format);
        var fmt = Assert.IsType<DateTimeFormatDescriptor>(col.Format);
        Assert.Equal("MM-dd-yyyy hh:mm:ss", fmt.Pattern);
    }

    [Fact]
    public void Deserialize_StringColumn_WithInt32Format_Returns_Int32FormatDescriptor()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""intAsString"",
                    ""type"": ""String"",
                    ""length"": { ""min"": 0, ""max"": 20 },
                    ""format"": { ""type"": ""Int32"" },
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<StringColumn>(schema.Columns[0]);
        Assert.NotNull(col.Format);
        Assert.IsType<Int32FormatDescriptor>(col.Format);
    }

    [Fact]
    public void Deserialize_StringColumn_WithDateOnlyFormat_Returns_DateOnlyFormatDescriptor()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""dateOnlyAsString"",
                    ""type"": ""String"",
                    ""length"": { ""min"": 0, ""max"": 20 },
                    ""format"": { ""type"": ""DateOnly"", ""pattern"": ""yyyy-MM-dd"" },
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<StringColumn>(schema.Columns[0]);
        Assert.NotNull(col.Format);
        Assert.IsType<DateOnlyFormatDescriptor>(col.Format);
    }

    [Fact]
    public void Deserialize_StringColumn_WithTimeOnlyFormat_Returns_TimeOnlyFormatDescriptor()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""timeOnlyAsString"",
                    ""type"": ""String"",
                    ""length"": { ""min"": 0, ""max"": 20 },
                    ""format"": { ""type"": ""TimeOnly"", ""pattern"": ""HH:mm:ss"" },
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<StringColumn>(schema.Columns[0]);
        Assert.NotNull(col.Format);
        Assert.IsType<TimeOnlyFormatDescriptor>(col.Format);
    }
}