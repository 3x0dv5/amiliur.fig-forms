using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Form.Components;

public partial class OoriFormTextField : OoriSimpleTypeFormFieldBase<string>
{
    [Parameter] public bool Multiline { get; set; }

    private string ValueWrapper
    {
        get => FValue ?? string.Empty;
        set => FValue = value;
    }

    public ComponentBase Control { get; set; } = null!;
}