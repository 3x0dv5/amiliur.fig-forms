using System.Text.Json.Serialization;
using amiliur.figforms.shared.Fields.Binding;
using amiliur.figforms.shared.Fields.Models.Renderers;
using amiliur.figforms.shared.Validation;
using amiliur.shared.Json;

namespace amiliur.figforms.shared.Fields.Models;

public abstract class BaseFormFieldModel : IFormField, ISerializableModel
{
    public IEnumerable<FormFieldValidation> ValidationRules { get; set; } = new List<FormFieldValidation>();
    public IEnumerable<FormFieldBinding> FieldBindings { get; set; } = new List<FormFieldBinding>();
    public string FieldName { get; set; }
    private string _displayName;

    public string DisplayName
    {
        get => string.IsNullOrEmpty(_displayName) ? FieldName : _displayName;
        set => _displayName = value;
    }

    public string Description { get; set; }

    public bool? IsRequired()
    {
        return ValidationRules.Any(m => m.GetType() == typeof(RequiredFormFieldValidation));
    }

    public bool IsVisible()
    {
        return !IsHidden;
    }

    protected FormFieldType fieldType;

    [JsonIgnore] public FormFieldType FieldType => fieldType;


    public IFormFieldRenderer? FieldRenderer { get; set; }

    public bool IsHidden { get; set; } = false;

    protected BaseFormFieldModel()
    {
    }

    protected BaseFormFieldModel(string fieldName) : this()
    {
        FieldName = fieldName;
    }

    protected BaseFormFieldModel(string fieldName, IEnumerable<FormFieldValidation> validations) : this(fieldName)
    {
        ValidationRules = validations;
    }
}
