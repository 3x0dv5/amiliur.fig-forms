using amiliur.figforms.shared;
using amiliur.figforms.web.blazor.Form.Components.Args;
using amiliur.web.shared.Models.Generic;
using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Form.Components;

public class RelatedFieldToMustUpdateArgs
{
    public string FieldToUpdate { get; set; }
    public object Value { get; set; }
}

public abstract class OoriFormField : ComponentBase
{
    [Parameter] public IFormField FormField { get; set; }
    [Parameter] public string FId { get; set; }
    [Parameter] public EventCallback<FieldChangedArgs> ValueChanged { get; set; }

    [Parameter] public IOoriFieldEventHandlers FieldEventHandlers { get; set; }
    [Parameter] public BaseEditModel FormInputModel { get; set; }
}

public abstract class OoriSimpleTypeFormFieldBase<TValue> : OoriFormField
{
    [Parameter] public TValue FValue { get; set; }

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

public abstract class OoriComplextTypeFormFieldBase<TValue> : OoriFormField
{
    [Parameter] public TValue FValue { get; set; }

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