using amiliur.figforms.shared.Fields.Models.Datasource;
using amiliur.figforms.shared.Fields.Models.Renderers;
using amiliur.figforms.shared.Validation;
using amiliur.shared.Json;

namespace amiliur.figforms.shared.Fields.Models;

public interface IWithRelatedModel
{
    public string ValueFieldName { get; set; }
}

public class DynamicFormSettings : ISerializableModel
{
    public string Context { get; set; }
    public string Code { get; set; }
    public string OnSaveUrl { get; set; } = "/form-data/save";
    public string OnSaveUrlMethod { get; set; } = "POST";

    public DynamicFormSettings()
    {
        
    }

    public DynamicFormSettings(string context, string code)
    {
        Context = context;
        Code = code;
    }
}

public class AddNewItemSettings : ISerializableModel
{
    public bool AllowCreateNew { get; set; } = false;
    public string AddButtonText { get; set; } = "Novo...";
    public string AddNewWindowTitle { get; set; } = "Novo...";
    public string AddNewControlFullName { get; set; }
    public DynamicFormSettings DynamicFormSettings { get; set; }
}

public class PickerFormFieldModel<TRelatedModel> : BaseFormFieldModel, IWithRelatedModel
{
    public string ValueFieldName { get; set; } = "Value";
    public string TextFieldName { get; set; } = "Text";
    public string StoreSelectionTextInField { get; set; }
    public AddNewItemSettings AddNewItemSettings { get; set; } = new();

    public IDatasource<TRelatedModel> Datasource { get; set; }


    public PickerFormFieldModel()
    {
        fieldType = FormFieldType.Picker;
        FieldRenderer = new PickerRenderer(typeof(TRelatedModel));
    }

    public PickerFormFieldModel(string fieldName) : base(fieldName)
    {
        fieldType = FormFieldType.Picker;
        FieldRenderer = new PickerRenderer(typeof(TRelatedModel));
    }

    public PickerFormFieldModel(string fieldName, IEnumerable<FormFieldValidation> validations) : base(fieldName, validations)
    {
        fieldType = FormFieldType.Picker;
        FieldRenderer = new PickerRenderer(typeof(TRelatedModel));
    }
}