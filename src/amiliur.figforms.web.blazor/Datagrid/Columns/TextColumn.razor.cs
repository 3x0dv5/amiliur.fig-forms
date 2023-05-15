using amiliur.web.shared.Attributes.Datagrid.Models;
using amiliur.web.shared.Attributes.Datagrid.Models.ColumnTypes;
using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Datagrid.Columns;

public partial class TextColumn<TGridItem>
{
    [Parameter] public GridColBase Column { get; set; }
    private TextCol col => (TextCol) Column;
}