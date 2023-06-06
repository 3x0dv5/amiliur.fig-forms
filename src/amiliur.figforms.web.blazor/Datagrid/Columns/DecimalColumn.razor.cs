using amiliur.web.shared.Attributes.Datagrid.Models.ColumnTypes;
using amiliur.web.shared.Attributes.Datagrid.Models;
using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Datagrid.Columns;

public partial class DecimalColumn<TGridItem>
{
    [Parameter] public GridColBase Column { get; set; }
    public DecimalCol col => (DecimalCol)Column;
}