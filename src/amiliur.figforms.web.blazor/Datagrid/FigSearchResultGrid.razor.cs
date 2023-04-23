using amiliur.figforms.shared.Attributes.Datagrid.Models;
using amiliur.figforms.shared.Attributes.Datagrid.SettingsReader;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace amiliur.figforms.web.blazor.Datagrid;

public partial class FigSearchResultGrid<TItem>: ComponentBase
{
    [Inject] IGridSettingsReader SettingsReader { get; set; }
    [Parameter] public List<TItem> DataSource { get; set; }
    public RadzenDataGrid<TItem> GridObj { get; set; }
    private DataGridSettings? Settings { get; set; }
    protected override void OnInitialized()
    {
        Settings = SettingsReader.Read(typeof(TItem));
        base.OnInitialized();
    }
}