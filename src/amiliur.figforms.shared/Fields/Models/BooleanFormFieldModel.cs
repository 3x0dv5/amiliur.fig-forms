using amiliur.figforms.shared.Validation;

namespace amiliur.figforms.shared.Fields.Models;

public class BooleanFormField : BaseFormFieldModel
{
    public BooleanFormField()
    {
        fieldType = FormFieldType.Boolean;
    }

    public BooleanFormField(string fieldName) : base(fieldName)
    {
        fieldType = FormFieldType.Boolean;
    }

    public BooleanFormField(string fieldName, IEnumerable<FormFieldValidation> validations) : base(fieldName, validations)
    {
        fieldType = FormFieldType.Boolean;
    }
}