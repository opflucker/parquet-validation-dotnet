using FluentValidation;

namespace ParquetValidation.Columns;

public class BaseColumnValidator<TColumn> : AbstractValidator<TColumn>
    where TColumn : BaseColumn
{
    public BaseColumnValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Column name must not be empty.");
    }
}