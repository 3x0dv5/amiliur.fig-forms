using amiliur.figforms.shared.Attributes.Datagrid.Enums;

namespace amiliur.figforms.shared.Attributes.Datagrid.Attributes.GridColAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class DeleteButtonGridColAttribute : ButtonGridColAttribute
{
    public DeleteButtonGridColAttribute()
    {
        TypeOfButton = TypeOfButton.Delete;
        Tooltip = "Remover";  // TODO: Translate
    }
}