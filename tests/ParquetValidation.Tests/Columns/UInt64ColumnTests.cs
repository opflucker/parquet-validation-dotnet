using ParquetValidation.Columns.Numbers;

namespace ParquetValidation.Tests.Columns;

public class UInt64ColumnTests
{
    [Fact]
    public void Deserialize_UInt64Column_WithValues_Returns_UInt64Column()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""myUInt64"",
                    ""type"": ""UInt64"",
                    ""range"": { ""min"": 0, ""max"": 18446744073709551615 },
                    ""required"": true
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<UInt64Column>(schema.Columns[0]);
        Assert.Equal("myUInt64", col.Name);
        Assert.True(col.Required);
    }
}