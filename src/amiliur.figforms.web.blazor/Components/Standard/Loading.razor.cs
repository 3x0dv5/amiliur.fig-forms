using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Components.Standard;

public partial class Loading
{
    [Parameter] public string Message { get; set; } = "Carregando..."; // todo: translate
}