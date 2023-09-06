using amiliur.figforms.web.blazor.Form.Components.Args;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;

namespace amiliur.figforms.web.blazor.Form.Components;

public abstract class OoriSimpleTypeFormFieldBase<TValue> : OoriFormField
{
    [Parameter] public TValue? FValue { get; set; }


    protected async Task OnValueChanged(ChangedEventArgs arg)
    {
        var argsToPass = new FieldChangedArgs
        {
            NewValue = arg.Value,
            OldValue = arg.PreviousValue,
            FormField = FormField,
            PropertyName = FormField.FieldName
        };
        FValue = (TValue)Convert.ChangeType(arg.Value, typeof(TValue));
        await FieldEventHandlers.OnFieldChanged(FormField, argsToPass);
    }

    protected async Task OnValueChanged(string? arg)
    {
        var argsToPass = new FieldChangedArgs
        {
            NewValue = arg,
            OldValue = FValue,
            FormField = FormField,
            PropertyName = FormField.FieldName
        };

        FValue = arg == null ? default : (TValue)Convert.ChangeType(arg, typeof(TValue));
        await FieldEventHandlers.OnFieldChanged(FormField, argsToPass);
    }
}