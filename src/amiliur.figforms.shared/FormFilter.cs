using amiliur.shared.Json;
using amiliur.web.shared.Filtering;

[Serializable]
public abstract class FilterExpr : ISerializableModel
{
    public WhereFilterType FilterType { get; set; }
}

[Serializable]
public class BinaryExpr : FilterExpr
{
    public FilterExpr Left { get; set; }
    public FilterExpr Right { get; set; }
}

[Serializable]
public class UnaryExpr : FilterExpr
{
    public UnaryExpr()
    {
    }

    public UnaryExpr(FilterExpr operand)
    {
        Operand = operand;
    }

    public FilterExpr Operand { get; set; } = null!;
}

[Serializable]
public class FieldValueExpr : FilterExpr
{
    public FieldValueExpr()
    {
    }

    public FieldValueExpr(string field, string value, WhereFilterType filterType)
    {
        Field = field;
        Value = value;
        FilterType = filterType;
    }

    public string? Field { get; set; }
    public object? Value { get; set; }
}