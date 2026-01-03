using ParquetValidation.Columns.Numbers;

namespace ParquetValidation.Tests.Columns;

public class FloatColumnTests
{
    [Fact]
    public void Deserialize_FloatColumn_WithValues_Returns_FloatColumn()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""myFloat"",
                    ""type"": ""Float"",
                    ""range"": { ""min"": 0.0, ""max"": 3.14 },
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<FloatColumn>(schema.Columns[0]);
        Assert.Equal("myFloat", col.Name);
        Assert.False(col.Required);
    }
}