using amiliur.figforms.shared.Attributes.Datagrid.Enums;

namespace amiliur.figforms.shared.Attributes.Datagrid.Attributes.GridColAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class LinkGridColAttribute : TextGridColAttribute
{
    public override TypeOfColumn TypeOfColumn => TypeOfColumn.Link;
    public string? LinkFormat { get; set; }
}