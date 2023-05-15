using amiliur.figforms.shared.Fields.Models;
using amiliur.shared.Extensions;

namespace amiliur.figforms.shared.Fields.Binding;

public class GenerateSlugBinding : FormFieldBinding
{
    public string SourceFieldName { get; set; }

    public override object GetValue(BaseFormFieldModel field, object dataObject)
    {
        var sourceValue = dataObject.GetType().GetProperty(SourceFieldName)?.GetValue(dataObject);
        return sourceValue?.ToString()?.GenerateSlug();
    }
}