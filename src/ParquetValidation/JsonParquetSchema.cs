using FluentValidation.Results;
using Parquet;
using Parquet.Schema;
using ParquetValidation.Columns;

namespace ParquetValidation;

public sealed class JsonParquetSchema
{
    public List<BaseColumn> Columns { get; set; } = [];

    public async Task<ValidationResult> Validate(ParquetReader parquetReader, int maxErrors)
    {
        var validationTypesResult = ValidateTypes(parquetReader.Schema);
        if (!validationTypesResult.IsValid)
        {
            return validationTypesResult;
        }

        return await ValidateData(parquetReader, maxErrors);
    }

    public ValidationResult ValidateTypes(ParquetSchema parquetSchema)
    {
        var validationResult = new ValidationResult();

        foreach (var column in Columns)
        {
            var parquetField = parquetSchema.Fields.FirstOrDefault(f => f.Name == column.Name);
            if (parquetField == null)
            {
                validationResult.Errors.Add(new ValidationFailure(column.Name, "Column not found in Parquet schema."));
                continue;
            }

            if (parquetField.SchemaElement == null)
            {
                validationResult.Errors.Add(new ValidationFailure(column.Name, "Parquet field SchemaElement is null."));
                continue;
            }

            var columnValidationResult = column.ValidateTypes(parquetField.SchemaElement);
            validationResult.Errors.AddRange(columnValidationResult.Errors);
        }

        return validationResult;
    }

    public async Task<ValidationResult> ValidateData(ParquetReader parquetReader, int maxErrors)
    {
        var validationResult = new ValidationResult();

        var dataFields = parquetReader.Schema.GetDataFields();
        var columnDataFields = Columns
            .Select(column => new { Column = column, DataField = dataFields.FirstOrDefault(df => df.Name == column.Name) })
            .ToList();

        var nullColumnDataFields = columnDataFields.Where(cdf => cdf.DataField == null).ToList();
        if (nullColumnDataFields.Count > 0)
        {
            foreach (var cdf in nullColumnDataFields.Take(maxErrors))
            {
                validationResult.Errors.Add(new ValidationFailure(cdf.Column.Name, $"DataField {cdf.Column.Name} not found in Parquet schema."));
            }

            return validationResult;
        }

        for (int rowGroupIndex = 0; rowGroupIndex < parquetReader.RowGroupCount; rowGroupIndex++)
        {
            using var rowGroupReader = parquetReader.OpenRowGroupReader(rowGroupIndex);
            foreach (var cdf in columnDataFields)
            {
                var columnData = await rowGroupReader.ReadColumnAsync(cdf.DataField!);
                for (int dataIndex = 0; dataIndex < columnData.Data.Length; dataIndex++)
                {
                    var value = columnData.Data.GetValue(dataIndex);
                    var columnValidationResult = cdf.Column.ValidateData(value);
                    if (columnValidationResult.IsValid)
                    {
                        continue;
                    }

                    columnValidationResult.Errors.ForEach(e => e.ErrorMessage = $"At row {dataIndex} in row-group {rowGroupIndex}, error: {e.ErrorMessage}");
                    var maxErrorsToTake = maxErrors - validationResult.Errors.Count;
                    validationResult.Errors.AddRange(columnValidationResult.Errors.Take(maxErrorsToTake));

                    if (validationResult.Errors.Count >= maxErrors)
                    {
                        return validationResult;
                    }
                }
            }
        }

        return validationResult;
    }
}
