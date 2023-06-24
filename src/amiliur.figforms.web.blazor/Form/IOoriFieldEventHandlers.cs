using amiliur.figforms.shared;
using amiliur.figforms.web.blazor.Form.Components.Args;

namespace amiliur.figforms.web.blazor.Form;

public interface IOoriFieldEventHandlers
{
    Task OnFieldChanged(IFormField field, FieldChangedArgs args);
}