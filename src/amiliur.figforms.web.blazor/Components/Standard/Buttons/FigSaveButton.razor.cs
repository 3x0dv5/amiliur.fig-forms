using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace amiliur.figforms.web.blazor.Components.Standard.Buttons;

public partial class FigSaveButton
{
    [Parameter]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public EventCallback<MouseEventArgs> Click { get; set; }


    [Parameter] public string Text { get; set; } = Buttons.FigSaveButtonDefaultText;
    [Parameter] public string BusyText { get; set; } = Buttons.FigSaveButtonDefaultBusyText;

    private bool IsBusy { get; set; }

    private async Task OnClick(MouseEventArgs obj)
    {
        IsBusy = true;
        await Click.InvokeAsync(obj);
        IsBusy = false;
    }
}