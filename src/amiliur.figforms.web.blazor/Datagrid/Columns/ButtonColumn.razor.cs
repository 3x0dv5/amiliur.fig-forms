using amiliur.figforms.web.blazor.Datagrid.Columns.Arguments;
using amiliur.web.shared.Attributes.Datagrid.Models;
using amiliur.web.shared.Attributes.Datagrid.Models.ColumnTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace amiliur.figforms.web.blazor.Datagrid.Columns;

public partial class ButtonColumn<TGridItem>
{
    [Parameter] public GridColBase Column { get; set; }
    [Parameter] public EventCallback<ButtonColumnClickEventArgs> OnClick { get; set; }
    
    private ButtonCol col => (ButtonCol) Column;

    private void EditRow(MouseEventArgs eventArgs, TGridItem value)
    {
        OnClick.InvokeAsync(new ButtonColumnClickEventArgs
        {
            ContextData = value,
            Col = col, 
            MouseEventArgs = eventArgs
        });
        
    }

    private Task DeleteRow(MouseEventArgs eventArgs, TGridItem value)
    {
        Console.WriteLine($"DeleteRow {value}");
        return Task.CompletedTask;
    }
}