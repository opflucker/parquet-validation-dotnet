using FluentValidation.Results;
using Parquet.Meta;
using ParquetValidation.Commons;

namespace ParquetValidation.Columns.Booleans;

public sealed class BooleanColumn : BaseColumn
{
    public override ValidationResult ValidateTypes(SchemaElement schemaElement)
        => schemaElement.ValidateTypes(nameof(BooleanColumn),
            physicalType => physicalType == Parquet.Meta.Type.BOOLEAN);
}
