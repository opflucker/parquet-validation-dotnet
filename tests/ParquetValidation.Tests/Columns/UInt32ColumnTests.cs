using ParquetValidation.Columns.Numbers;

namespace ParquetValidation.Tests.Columns;

public class UInt32ColumnTests
{
    [Fact]
    public void Deserialize_UInt32Column_WithValues_Returns_UInt32Column()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""myUInt32"",
                    ""type"": ""UInt32"",
                    ""range"": { ""min"": 0, ""max"": 4294967295 },
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<UInt32Column>(schema.Columns[0]);
        Assert.Equal("myUInt32", col.Name);
        Assert.False(col.Required);
    }
}