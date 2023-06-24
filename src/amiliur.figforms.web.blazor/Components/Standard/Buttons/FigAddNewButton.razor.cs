using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using amiliur.shared.Claims;

namespace amiliur.figforms.web.blazor.Components.Standard.Buttons;

public partial class FigAddNewButton : BaseButton
{
    [Parameter] public string Text { get; set; } = Buttons.FigAddNewButtonText;
    [Parameter] public string BusyText { get; set; } = Buttons.FigAddNewButtonBusyText;
    [Parameter] public string? Url { get; set; }
    private bool IsBusy { get; set; }
    protected override string ClaimValue => FigClaimValues.Add;

    private async Task Submit(MouseEventArgs e)
    {
        IsBusy = true;
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(e);
        }

        if (!string.IsNullOrEmpty(Url))
        {
            NavManager.NavigateTo(Url);
        }

        IsBusy = false;
    }
}