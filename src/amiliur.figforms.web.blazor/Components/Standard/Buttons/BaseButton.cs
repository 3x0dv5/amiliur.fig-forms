using amiliur.web.blazor.Services.AppState;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Radzen;

namespace amiliur.figforms.web.blazor.Components.Standard.Buttons;

public abstract class BaseButton : ComponentBase
{
    [Inject] protected NavigationManager? NavManager { get; set; }
    [Inject] protected AppStateService? AppStateService { get; set; }
    [Parameter] public bool Visible { get; set; } = true;
    [Parameter] public string? Claim { get; set; }
    [Parameter] public bool Small { get; set; }
    [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Medium;

    [Parameter]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    
    
    protected abstract string ClaimValue { get; }

    protected bool ShowButton { get; set; }

    private async Task<bool> IsToShowButton()
    {
        if (!Visible) return false;

        if (string.IsNullOrEmpty(Claim))
            return Visible;

        if (AppStateService == null)
            return false;
        return await AppStateService.HasClaimValue(Claim, ClaimValue);
    }

    protected override async Task OnParametersSetAsync()
    {
        ShowButton = await IsToShowButton();
        StateHasChanged();
    }
}