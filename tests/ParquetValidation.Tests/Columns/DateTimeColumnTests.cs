using ParquetValidation.Columns.Dates;

namespace ParquetValidation.Tests.Columns;

public class DateTimeColumnTests
{
    [Fact]
    public void Deserialize_DateTimeColumn_WithValues_Returns_DateTimeColumn()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""myDateTime"",
                    ""type"": ""DateTime"",
                    ""range"": { ""min"": ""2025-01-01T00:00:00"", ""max"": ""2025-12-31T23:59:59"" },
                    ""required"": true
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<DateTimeColumn>(schema.Columns[0]);
        Assert.Equal("myDateTime", col.Name);
        Assert.True(col.Required);
    }
}