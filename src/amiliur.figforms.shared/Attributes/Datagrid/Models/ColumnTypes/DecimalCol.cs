using amiliur.figforms.shared.Attributes.Datagrid.Enums;

namespace amiliur.figforms.shared.Attributes.Datagrid.Models.ColumnTypes;

public class DecimalCol : GridColBase
{
    public override TypeOfColumn TypeOfColumn => TypeOfColumn.Decimal;

    public int DecimalPlaces { get; set; } = 2;

    public string Format => $"N{DecimalPlaces}";

    public DecimalCol(string field, string name) : base(field, name)
    {
    }
}
