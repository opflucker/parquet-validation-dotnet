using ParquetValidation.Columns.Numbers;

namespace ParquetValidation.Tests.Columns;

public class Int64ColumnTests
{
    [Fact]
    public void Deserialize_Int64Column_WithValues_Returns_Int64Column()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""myInt64"",
                    ""type"": ""Int64"",
                    ""range"": { ""min"": 0, ""max"": 10000000000 },
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<Int64Column>(schema.Columns[0]);
        Assert.Equal("myInt64", col.Name);
        Assert.False(col.Required);
    }
}