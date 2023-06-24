using amiliur.figforms.shared.Fields.Models.Renderers;
using amiliur.figforms.shared.Validation;

namespace amiliur.figforms.shared.Fields.Models;

public enum NumberType
{
    Integer,
    Long,
    Float,
    Double,
    Decimal 
}

public class NumericFormFieldModel : BaseFormFieldModel
{
    public NumericFormFieldModel()
    {
        fieldType = FormFieldType.Numeric;
        FieldRenderer = new NumericFieldRenderer();
    }
    public NumericFormFieldModel(Type genericType)
    {
        fieldType = FormFieldType.Numeric;
        FieldRenderer = new NumericFieldRenderer(genericType);
    }

    public NumericFormFieldModel(string fieldName, Type genericType) : base(fieldName)
    {
        fieldType = FormFieldType.Numeric;
        FieldRenderer = new NumericFieldRenderer(genericType);
    }

    public NumericFormFieldModel(string fieldName, Type genericType, IEnumerable<FormFieldValidation> validations) : base(fieldName, validations)
    {
        fieldType = FormFieldType.Numeric;
        FieldRenderer = new NumericFieldRenderer(genericType);
    }
}