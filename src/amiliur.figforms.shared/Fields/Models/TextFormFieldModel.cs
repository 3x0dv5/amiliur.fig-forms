using amiliur.figforms.shared.Fields.Models.Renderers;
using amiliur.figforms.shared.Validation;

namespace amiliur.figforms.shared.Fields.Models;

public class TextFormFieldModel : BaseFormFieldModel
{
    public bool Multiline { get; set; } = false;
    public bool ReadOnly { get; set; } = false;
    public TextFormFieldModel()
    {
        fieldType = FormFieldType.Text;
        FieldRenderer = new TextFieldRenderer();
    }

    public TextFormFieldModel(string fieldName) : base(fieldName)
    {
        fieldType = FormFieldType.Text;
        FieldRenderer = new TextFieldRenderer();
    }
    
    public TextFormFieldModel(string fieldName, IEnumerable<FormFieldValidation> validations) : base(fieldName, validations)
    {
        fieldType = FormFieldType.Text;
        FieldRenderer = new TextFieldRenderer();
    }
    
}