using amiliur.web.shared.Attributes.Datagrid.Enums;
using amiliur.web.shared.Attributes.Datagrid.Models.ColumnTypes;
using Microsoft.AspNetCore.Components.Web;

namespace amiliur.figforms.web.blazor.Datagrid.Columns.Arguments;

public class ButtonColumnClickEventArgs
{
    public MouseEventArgs MouseEventArgs { get; set; }
    public object ContextData { get; set; }
    public string ClickedPropertyName => Col.Field;
    public TypeOfButton TypeOfButton => Col.TypeOfButton;
    public ButtonCol Col { get; set; }
}