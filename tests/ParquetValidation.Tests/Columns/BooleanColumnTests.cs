using ParquetValidation.Columns.Booleans;

namespace ParquetValidation.Tests.Columns;

public class BooleanColumnTests
{
    [Fact]
    public void Deserialize_BooleanColumn_Returns_BooleanColumn()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""isActive"",
                    ""type"": ""Boolean"",
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<BooleanColumn>(schema.Columns[0]);
        Assert.Equal("isActive", col.Name);
        Assert.False(col.Required);
    }
}