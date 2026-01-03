# Apache Parquet Validation for .NET
.NET library to validate parquet files schema and data against a json schema definition.

# Quick start
ParquetValidation library is designed to validate parquet files schema and data against a json shcema definition. This functionality enables develop tools geared towards infraestructure technicians, freeing code developers from the task of validating data sources "after deployment" and enter in the time-consuming loop deploy-check-fix-redeploy. Using JSON to declare expected parquet schemas allows:

- Easy schema definition, maintenance and sharing.
- Drive integration work through contracts.
- Facilitate develop of additional tools like code generators or health-chekers.

## Declare a json schema
The json schema has the following structure:
```json
{
  "Columns": [
	{
	  "Name": "ColumnName",
	  "Required": true|false,
	  "Type": "DataType"
	}
  ]
}
```
Fields "name" and "type" are mandatory. Field "required" is optional and defaults to false.
Depending on field "type", additional properties can be defined.

Sample json schema:
```json
{
  "Columns": [
	{
	  "Name": "Id",
	  "Required": true,
	  "Type": "Int64",
	  "Range": { "Min": 1, "Max": 1000000 }
	},
	{
	  "Name": "Name",
	  "Type": "String"
	},
	{
	  "Name": "BirthDate",
	  "Type": "DateTime"
	},
	{
	  "Name": "IsActive",
	  "Type": "Boolean"
	}
  ]
}
```

## Load json schema
The json schema can be loaded from from a string using class JsonParquetSchemaReader. Example:
```csharp
var jsonSchemaString = File.ReadAllText("schema.json");
var jsonSchema = JsonParquetSchemaReader.ReadFrom(jsonSchema);
```

## Validate parquet file
The parquet file can be validated using class JsonParquetSchemaValidator. Example:
```csharp
var jsonValidator = new JsonParquetSchemaValidator();
var result = jsonValidator.Validate(jsonSchema);
if (result.IsValid)
{
	Console.WriteLine("Parquet file is valid.");
}
else
{
	Console.WriteLine("Parquet file is invalid. Errors:");
	foreach (var failure in result.Errors)
	{
		Console.WriteLine($"- {failure.PropertyName}: {failure.ErrorMessage}");
	}
}
```

# Dependencies
ParquetValidation has following main dependencies:
- [Parquet.Net](https://github.com/aloneguid/parquet-dotnet) for parquet file reading.
- [FluentValidation](https://github.com/FluentValidation/FluentValidation) for validation results.

# Test projects
Three test projects are included:
- ParquetValidation.Tests: Unit tests for ParquetValidation library.
- ParquetValidation.ConsoleTest: Console application with examples of usage.
- ParquetValidation.WebApiTest: Web api exposing and endpoint for validating parquet files stored in Azure Data Lake.
