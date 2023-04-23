using amiliur.figforms.shared.Attributes.Datagrid.Attributes.GridColAttributes;

namespace Fig.App.Web.Shared.DynUi.GridColAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class IntGridColAttribute : DecimalGridColAttribute
{
    public IntGridColAttribute()
    {
        DecimalPlaces = 0;
    }
}