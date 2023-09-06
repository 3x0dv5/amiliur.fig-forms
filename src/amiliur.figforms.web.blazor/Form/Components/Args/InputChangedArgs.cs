using amiliur.figforms.shared;
using amiliur.web.shared.Models.Results;

namespace amiliur.figforms.web.blazor.Form.Components.Args;

public class InputChangedArgs
{
    public string PropertyName { get; set; } = null!;
    public object NewValue { get; set; } = null!;
    public object OldValue { get; set; } = null!;
    public object ObjectContainer { get; set; } = null!;
}

public class FormDefinitionLoadedArgs
{
    public FormDefinition FormDefinition { get; set; } = null!;
}

public class FormDataLoadedArgs
{
    public FormDefinition FormDefinition { get; set; } = null!;
    public object Data { get; set; } = null!;
}

public class FormDataSavedArgs
{
    public FormDefinition FormDefinition { get; set; } = null!;
    public object Data { get; set; } = null!;
    public SaveBaseResult Result { get; set; } = null!;
}