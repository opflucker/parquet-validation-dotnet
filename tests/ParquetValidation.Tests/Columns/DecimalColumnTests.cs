using ParquetValidation.Columns.Numbers;

namespace ParquetValidation.Tests.Columns;

public class DecimalColumnTests
{
    [Fact]
    public void Deserialize_DecimalColumn_WithValues_Returns_DecimalColumn()
    {
        var json = @"
        {
            ""columns"": [
                {
                    ""name"": ""myDecimal"",
                    ""type"": ""Decimal"",
                    ""range"": { ""min"": 0.00, ""max"": 99.99 },
                    ""required"": false
                }
            ]
        }";

        var schema = JsonParquetSchemaReader.ReadFrom(json);

        Assert.NotNull(schema);
        Assert.Single(schema.Columns);
        var col = Assert.IsType<DecimalColumn>(schema.Columns[0]);
        Assert.Equal("myDecimal", col.Name);
        Assert.False(col.Required);
    }
}