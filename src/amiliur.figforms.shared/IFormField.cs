using amiliur.figforms.shared.Fields;
using amiliur.figforms.shared.Fields.Models.Renderers;

namespace amiliur.figforms.shared;

public interface IFormField
{
    public FormFieldType FieldType { get; }
    public IFormFieldRenderer FieldRenderer { get; }
    string FieldName { get; }
    string DisplayName { get; }
    bool? IsRequired();

    bool IsVisible();
}