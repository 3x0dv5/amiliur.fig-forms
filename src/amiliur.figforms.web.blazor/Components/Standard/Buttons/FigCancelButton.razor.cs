using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace amiliur.figforms.web.blazor.Components.Standard.Buttons;

public partial class FigCancelButton
{
    [Parameter]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public EventCallback<MouseEventArgs> Click { get; set; }


    [Parameter] public string Text { get; set; } = Buttons.FigCancelButtonText;
    [Parameter] public string BusyText { get; set; } = Buttons.FigCancelButtonBusyText;
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    [Inject] protected NavigationManager NavManager { get; set; }
    [Parameter] public string Url { get; set; }
    private bool IsBusy { get; set; }

    private async Task OnClick(MouseEventArgs obj)
    {
        IsBusy = true;
        if (Click.HasDelegate)
            await Click.InvokeAsync(obj);
        else
        {
            if (string.IsNullOrEmpty(Url))
                await JsRuntime.InvokeVoidAsync("NavigateBack");
            else
            {
                NavManager.NavigateTo(Url, new NavigationOptions {ReplaceHistoryEntry = true});
            }
        }

        IsBusy = false;
    }
}