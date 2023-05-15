using amiliur.figforms.shared.Fields.Models.Renderers;
using amiliur.figforms.shared.Validation;

namespace amiliur.figforms.shared.Fields.Models;

public class HiddenFormFieldModel : BaseFormFieldModel
{
    public HiddenFormFieldModel()
    {
        fieldType = FormFieldType.Text;
        IsHidden = true;
        FieldRenderer = new HiddenRenderer();
    }

    public HiddenFormFieldModel(string fieldName) : base(fieldName)
    {
        fieldType = FormFieldType.Text;
        IsHidden = true;
        FieldRenderer = new HiddenRenderer();
    }

    public HiddenFormFieldModel(string fieldName, IEnumerable<FormFieldValidation> validations) : base(fieldName, validations)
    {
        fieldType = FormFieldType.Text;
        IsHidden = true;
        FieldRenderer = new HiddenRenderer();
    }
}