using amiliur.figforms.shared.Fields.Models;
using amiliur.shared.Json;

namespace amiliur.figforms.shared.Fields.Binding;

public abstract class FormFieldBinding: ISerializableModel
{
    public abstract object GetValue(BaseFormFieldModel field, object dataObject);
}