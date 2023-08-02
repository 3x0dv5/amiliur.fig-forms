using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;

namespace amiliur.figforms.web.blazor.Components.Standard.Buttons;

public partial class FigCancelButton
{
    [Parameter]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public EventCallback<MouseEventArgs> Click { get; set; }


    [Parameter] public string Text { get; set; } = Buttons.FigCancelButtonText;
    [Parameter] public string BusyText { get; set; } = Buttons.FigCancelButtonBusyText;
    [Parameter] public string Url { get; set; } = null!;
    [Parameter] public bool IsInPopup { get; set; }
    [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] protected NavigationManager NavManager { get; set; } = null!;
    [Inject] protected DialogService DialogService { get; set; } = null!;
    
    private bool IsBusy { get; set; }

    private async Task OnClick(MouseEventArgs obj)
    {
        IsBusy = true;
        if (Click.HasDelegate)
            await Click.InvokeAsync(obj);
        else
        {
            if (IsInPopup)
            {
                DialogService.Close();
            }
            else
            {
                if (string.IsNullOrEmpty(Url))
                    await JsRuntime.InvokeVoidAsync("NavigateBack");
                else
                {
                    NavManager.NavigateTo(Url, new NavigationOptions { ReplaceHistoryEntry = true });
                }
            }
           
        }

        IsBusy = false;
    }
}