using amiliur.figforms.shared.Attributes.Datagrid.Enums;

namespace amiliur.figforms.shared.Attributes.Datagrid.Models.ColumnTypes;

public class DateCol : GridColBase
{
    public override TypeOfColumn TypeOfColumn => TypeOfColumn.Date;

    public DateCol(string field, string name) : base(field, name)
    {
    }
}
