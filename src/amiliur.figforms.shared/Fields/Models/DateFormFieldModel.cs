using amiliur.figforms.shared.Fields.Models.Renderers;
using amiliur.figforms.shared.Validation;
using JetBrains.Annotations;

namespace amiliur.figforms.shared.Fields.Models;

[UsedImplicitly]
public class DateFormFieldModel : BaseFormFieldModel
{
    public CalendarView StartView { get; set; } = CalendarView.Year;
    public CalendarView DepthView { get; set; } = CalendarView.Year;


    public DateFormFieldModel()
    {
        fieldType = FormFieldType.Date;
        FieldRenderer = new DateFieldRenderer();
    }

    public DateFormFieldModel(string fieldName) : base(fieldName)
    {
        fieldType = FormFieldType.Date;
        FieldRenderer = new DateFieldRenderer();
    }

    public DateFormFieldModel(string fieldName, IEnumerable<FormFieldValidation> validations) : base(fieldName, validations)
    {
        fieldType = FormFieldType.Date;
        FieldRenderer = new DateFieldRenderer();
    }
}