using amiliur.figforms.web.blazor.Form.Components.Args;
using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Form.Components;

public class RelatedFieldToMustUpdateArgs
{
    public string FieldToUpdate { get; set; }= null!;
    public object? Value { get; set; }
}

public abstract class OoriComplextTypeFormFieldBase<TValue> : OoriFormField
{
    [Parameter] public TValue? FValue { get; set; }

    protected async Task OnValueChanged(TValue value)
    {
        var argsToPass = new FieldChangedArgs
        {
            NewValue = value,
            OldValue = FValue,
            FormField = FormField,
            PropertyName = FormField.FieldName
        };
        FValue = value;
        await FieldEventHandlers.OnFieldChanged(FormField, argsToPass);
    }
}