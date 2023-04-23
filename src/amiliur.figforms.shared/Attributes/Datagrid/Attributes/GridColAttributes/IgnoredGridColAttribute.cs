using amiliur.figforms.shared.Attributes.Datagrid.Enums;

namespace amiliur.figforms.shared.Attributes.Datagrid.Attributes.GridColAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class IgnoredGridColAttribute : GridColAttribute
{
    public override TypeOfColumn TypeOfColumn => TypeOfColumn.Ignored;
}