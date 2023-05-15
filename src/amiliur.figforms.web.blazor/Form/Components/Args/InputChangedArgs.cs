using amiliur.figforms.shared;

namespace amiliur.figforms.web.blazor.Form.Components.Args;

public class InputChangedArgs
{
    public string PropertyName { get; set; }
    public object NewValue { get; set; }
    public object OldValue { get; set; }
    public object ObjectContainer { get; set; }
}

public class FormDefinitionLoadedArgs
{
    public FormDefinition FormDefinition { get; set; }
}
public class FormDataLoadedArgs
{
    public FormDefinition FormDefinition { get; set; }
    public Object Data { get; set; }
}