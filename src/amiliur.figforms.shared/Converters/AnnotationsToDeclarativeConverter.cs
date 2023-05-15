using System.Reflection;
using amiliur.figforms.shared.Fields.Binding;
using amiliur.figforms.shared.Fields.Models;
using amiliur.figforms.shared.Validation;
using amiliur.shared.Extensions;
using amiliur.web.shared.Attributes;
using amiliur.web.shared.Forms;

namespace amiliur.figforms.shared.Converters;

public class AnnotationsToDeclarativeConverter
{
    public static ICollection<FormDefinition> Convert(Type type)
    {
        var fDefAttrs = type.GetCustomAttributes<FormDefinitionAttribute>();
        var formDefinitions = new List<FormDefinition>();

        foreach (var fDefAttr in fDefAttrs)
        {
            var formDefinition = new FormDefinition(fDefAttr.FormContext, fDefAttr.FormModule, fDefAttr.FormCode, fDefAttr.FormName, fDefAttr.FormMode, fDefAttr.FormTitle)
            {
                FormDescription = fDefAttr.Description
            };
            
            var fields = GetFormFieldAttributes(type, fDefAttr.FormMode);
            foreach (var prop in fields.Keys)
            {
                var f = ConvertFieldAnnotationToFieldDefinition(prop, fields[prop], fDefAttr.FormMode);
                f.ValidationRules = GetFieldValidationRules(prop, f, fDefAttr.FormMode);
                f.FieldBindings = GetFieldBindings(prop, f, fDefAttr.FormMode);
                formDefinition.AddField(f);
            }

            formDefinitions.Add(formDefinition);
        }

        return formDefinitions;
    }

  

    private static Dictionary<PropertyInfo, BaseFormFieldAttribute> GetFormFieldAttributes(Type type, FormMode formMode)
    {
        return type
            .GetProperties()
            .Select(
                m => new
                {
                    Property = m,
                    FormFieldAttr = m.GetCustomAttribute<BaseFormFieldAttribute>()
                }
            ).Where(m =>
                m.FormFieldAttr != null
                && (
                    m.FormFieldAttr.FormMode == formMode
                    || m.FormFieldAttr.FormMode == null)
            )
            .ToDictionary(k => k.Property, v => v.FormFieldAttr);
    }


    private static BaseFormFieldModel ConvertFieldAnnotationToFieldDefinition(PropertyInfo property, BaseFormFieldAttribute annotation, FormMode formMode)
    {
        return annotation switch
        {
            TextFormFieldAttribute fieldAnnotation => ConvertTextFieldAnnotationToFieldDefinition(property, fieldAnnotation, formMode),
        };
    }

    private static TextFormFieldModel ConvertTextFieldAnnotationToFieldDefinition(PropertyInfo property, TextFormFieldAttribute annotation, FormMode formMode)
    {
        var field = new TextFormFieldModel(property.Name)
        {
            DisplayName = string.IsNullOrEmpty(annotation.Label) ? property.DisplayName() : annotation.Label,
            Description = annotation.Description,
            IsHidden = annotation.IsHidden,
            Multiline = annotation.Multiline,
            ReadOnly = annotation.ReadOnly
        };

        field.ValidationRules = GetFieldValidationRules(property, field, formMode);
        return field;
    }

    private static IEnumerable<FormFieldValidation> GetFieldValidationRules(PropertyInfo property, BaseFormFieldModel field, FormMode formMode)
    {
        var validations = new List<FormFieldValidation>();
        AppendValidation(GetRequiredValidation(property, field, formMode), validations);
        return validations;
    }
    private static IEnumerable<FormFieldBinding> GetFieldBindings(PropertyInfo prop, BaseFormFieldModel formField, FormMode formMode)
    {
        var bindings = new List<FormFieldBinding>();
        AppendBinding(GetFieldBinding(prop, formField, formMode), bindings);
        return bindings;
    }
    private static FormFieldBinding GetFieldBinding(PropertyInfo prop, BaseFormFieldModel formField, FormMode formMode)
    {
        var bindingAttr = prop.GetCustomAttribute<GenerateSlugAttribute>();
        if (bindingAttr != null)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (bindingAttr.FormModes == null || bindingAttr.FormModes.Length == 0 || bindingAttr.FormModes.Contains(formMode))
            {
                var binding = new GenerateSlugBinding
                {
                    SourceFieldName = bindingAttr.SourceField
                };
                return binding;
            }
        }
        return null;
    }
    private static void AppendBinding(FormFieldBinding binding, List<FormFieldBinding> bindings)
    {
        if (binding!= null)
            bindings.Add(binding);
    }
   
    private static void AppendValidation(FormFieldValidation validation, ICollection<FormFieldValidation> validations)
    {
        if (validation != null)
            validations.Add(validation);
    }

    private static FormFieldValidation GetRequiredValidation(PropertyInfo property, BaseFormFieldModel field, FormMode formMode)
    {
        var requiredAttr = property.GetCustomAttribute<RequiredFieldAttribute>();
        if (requiredAttr != null)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (requiredAttr.FormModes == null || requiredAttr.FormModes.Length == 0 || requiredAttr.FormModes.Contains(formMode))
                return new RequiredFormFieldValidation
                {
                    ErrorMessageString = $"The field `{field.DisplayName}` is required"
                };
        }

        return null;
    }
}