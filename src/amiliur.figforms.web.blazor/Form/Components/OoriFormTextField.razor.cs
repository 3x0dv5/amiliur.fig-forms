using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace amiliur.figforms.web.blazor.Form.Components;

public partial class OoriFormTextField : OoriSimpleTypeFormFieldBase<string>
{
    [Parameter] public bool Multiline { get; set; }

    private string ValueWrapper
    {
        get => FValue;
        set => FValue = value;
    }
    public ComponentBase Control { get; set; }
}