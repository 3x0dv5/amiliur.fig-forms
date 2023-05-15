using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace amiliur.figforms.web.blazor.Components.Standard.Buttons;

public partial class FigButtonBar
{
    
    [Parameter] public RenderFragment ChildContent { get; set; } = null!;
}