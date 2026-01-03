using Parquet;
using ParquetValidation;
using System.Text.Json;

var json = @"
{
    ""columns"": [
        {
            ""name"": ""Field1"",
            ""type"": ""String""
        },
		{
			""name"": ""Field2"",
			""type"": ""String"",
			""format"": { ""type"": ""Int32"" }
		},
		{
			""name"": ""Field3"",
			""type"": ""Date""
		},
		{
			""name"": ""Field4"",
			""type"": ""Int32"",
			""range"": { ""min"": 0, ""max"": 23 }
		},
		{
			""name"": ""Field5"",
			""type"": ""Decimal""
		},
		{
			""name"": ""Field6"",
			""type"": ""String"",
			""format"": { ""type"": ""DateTime"", ""pattern"": ""yyyy-MM-dd HH:mm:ss"" },
			""required"": true
		}
	]
}";

var jsonSchema = JsonParquetSchemaReader.ReadFrom(json);
Console.WriteLine(JsonSerializer.Serialize(jsonSchema, new JsonSerializerOptions { WriteIndented = true }));

var jsonValidator = new JsonParquetSchemaValidator();
var result = jsonValidator.Validate(jsonSchema);
if (result.IsValid)
{
    Console.WriteLine("Schema is valid.");

    using Stream fs = File.OpenRead("C:\\samples\\sample1.parquet");
    using ParquetReader reader = await ParquetReader.CreateAsync(fs);
    var parquetSchemaValidationResult = await jsonSchema.Validate(reader, 2);
    if (parquetSchemaValidationResult.IsValid)
    {
        Console.WriteLine("Parquet data is valid.");
    }
    else
    {
        Console.WriteLine("Parquet data is invalid:");
        PrintResults(parquetSchemaValidationResult);
    }
}
else
{
    Console.WriteLine("Schema is invalid:");
    PrintResults(result);
}

static void PrintResults(FluentValidation.Results.ValidationResult result)
{
    foreach (var failure in result.Errors)
    {
        Console.WriteLine($"- {failure.PropertyName}: {failure.ErrorMessage}");
    }
}