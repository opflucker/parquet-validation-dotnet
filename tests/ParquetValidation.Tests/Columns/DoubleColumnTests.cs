using ParquetValidation.Columns.Numbers;

namespace ParquetValidation.Tests.Columns;

public class DoubleColumnTests
{
    [Fact]
    public void Deserialize_DoubleColumn_WithValues_Returns_DoubleColumn()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""myDouble"",
                    ""type"": ""Double"",
                    ""range"": { ""min"": 0.0, ""max"": 1.2345 },
                    ""required"": true
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<DoubleColumn>(schema.Columns[0]);
        Assert.Equal("myDouble", col.Name);
        Assert.True(col.Required);
    }
}