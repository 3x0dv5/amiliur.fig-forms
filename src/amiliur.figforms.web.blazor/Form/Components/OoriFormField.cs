using amiliur.figforms.shared;
using amiliur.figforms.web.blazor.Form.Components.Args;
using amiliur.web.shared.Models.Generic;
using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Form.Components;

public abstract class OoriFormField : ComponentBase
{
    /// <summary>
    /// These parameters are set by the FigForm component using the DynamicComponent
    /// </summary>
    [Parameter] public IFormField FormField { get; set; } = null!;
    [Parameter] public string FId { get; set; } = null!;
    [Parameter] public EventCallback<FieldChangedArgs> ValueChanged { get; set; }

    [Parameter] public IOoriFieldEventHandlers FieldEventHandlers { get; set; } = null!;
    [Parameter] public BaseEditModel FormInputModel { get; set; } = null!;
    [Parameter] public bool IsHidden { get; set; }
}