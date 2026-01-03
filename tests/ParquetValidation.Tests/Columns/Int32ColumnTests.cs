using ParquetValidation.Columns.Numbers;

namespace ParquetValidator.Tests.Columns;

public class Int32ColumnTests
{
    [Fact]
    public void Deserialize_Int32Column_WithValues_Returns_Int32Column()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""myInt32"",
                    ""type"": ""Int32"",
                    ""range"": { ""min"": 0, ""max"": 100 },
                    ""required"": true
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<Int32Column>(schema.Columns[0]);
        Assert.Equal("myInt32", col.Name);
        Assert.True(col.Required);
    }
}