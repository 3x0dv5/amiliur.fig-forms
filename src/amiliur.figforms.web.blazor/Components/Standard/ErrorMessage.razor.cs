using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Components.Standard;

public partial class ErrorMessage
{
    [Parameter] public string? Message { get; set; }
}