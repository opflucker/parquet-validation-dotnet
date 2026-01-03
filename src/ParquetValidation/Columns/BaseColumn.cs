using FluentValidation.Results;
using Parquet.Meta;
using System.Text.Json.Serialization;

namespace ParquetValidation.Columns;

[JsonConverter(typeof(BaseColumnJsonConverter))]
public abstract class BaseColumn
{
    public string Name { get; set; } = string.Empty;
    public bool Required { get; set; }

    public virtual ValidationResult ValidateTypes(SchemaElement schemaElement)
        => new();

    public virtual ValidationResult ValidateData(object? data)
        => data is null ? ValidateNullData()
        : new();

    protected ValidationResult ValidateNullData()
        => Required 
        ? new([new ValidationFailure(Name, "Value is required but was null.")])
        : new();
}
