using amiliur.figforms.shared;

namespace amiliur.figforms.web.blazor.Form.Components.Args;

public class FieldChangedArgs
{
    public string PropertyName { get; set; } = string.Empty;
    public object? NewValue { get; set; }
    public object? OldValue { get; set; }
    public IFormField FormField { get; set; } = null!;
}