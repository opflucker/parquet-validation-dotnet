using ParquetValidation.Columns.Dates;

namespace ParquetValidation.Tests.Columns;

public class DateColumnTests
{
    [Fact]
    public void Deserialize_DateColumn_WithValues_Returns_DateColumn()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""myDate"",
                    ""type"": ""Date"",
                    ""range"": { ""min"": ""2025-01-01"", ""max"": ""2025-12-31"" },
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<DateColumn>(schema.Columns[0]);
        Assert.Equal("myDate", col.Name);
        Assert.False(col.Required);
    }
}