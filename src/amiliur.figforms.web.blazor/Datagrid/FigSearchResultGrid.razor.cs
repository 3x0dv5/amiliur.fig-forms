using amiliur.figforms.web.blazor.Datagrid.Columns.Arguments;
using amiliur.shared.Json;
using amiliur.web.shared.Attributes.Datagrid.Enums;
using amiliur.web.shared.Attributes.Datagrid.Models;
using amiliur.web.shared.Attributes.Datagrid.SettingsReader;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace amiliur.figforms.web.blazor.Datagrid;

public partial class FigSearchResultGrid<TItem> : ComponentBase
{
    [Inject] IGridSettingsReader SettingsReader { get; set; }
    [Parameter] public List<TItem> DataSource { get; set; }
    [Parameter] public EventCallback<ButtonColumnClickEventArgs> OnDeleteColumnClick { get; set; }

    [Parameter] public EventCallback<ButtonColumnClickEventArgs> OnEditColumnClick { get; set; }
    public RadzenDataGrid<TItem> GridObj { get; set; }
    private DataGridSettings? Settings { get; set; }

    protected override void OnInitialized()
    {
        Settings = SettingsReader.Read(typeof(TItem));
        base.OnInitialized();
    }

    private async Task OnGridButtonClick(ButtonColumnClickEventArgs btnArgs)
    {
        switch (btnArgs.TypeOfButton)
        {
            case TypeOfButton.Edit:
                await OnEditClick(btnArgs);
                break;
            case TypeOfButton.Delete:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private async Task OnEditClick(ButtonColumnClickEventArgs btnArgs)
    {
        if (!OnEditColumnClick.HasDelegate)
            throw new Exception("OnEditColumnClick is not set");
        await OnEditColumnClick.InvokeAsync(btnArgs);
    }

    private async Task OnDeleteClick(ButtonColumnClickEventArgs btnArgs)
    {
        if (!OnDeleteColumnClick.HasDelegate)
            throw new Exception("OnDeleteColumnClick is not set");
        await OnDeleteColumnClick.InvokeAsync(btnArgs);
    }
}