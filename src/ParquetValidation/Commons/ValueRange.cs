namespace ParquetValidation.Commons;

public sealed class ValueRange<T>
    where T : struct
{
    public T? Min { get; set; }
    public T? Max { get; set; }
}