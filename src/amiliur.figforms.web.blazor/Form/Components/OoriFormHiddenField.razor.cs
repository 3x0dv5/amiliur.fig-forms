using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Form.Components;

public partial class OoriFormHiddenField : OoriSimpleTypeFormFieldBase<string>
{
    private string ValueWrapper
    {
        get => FValue ?? string.Empty;
        set => FValue = value;
    }

    private async Task _OnValueChange(ChangeEventArgs args)
    {
        await OnValueChanged(args.Value?.ToString());
    }
}