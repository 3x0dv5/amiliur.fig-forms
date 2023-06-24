using amiliur.figforms.shared;
using amiliur.figforms.shared.Fields.Models;
using amiliur.figforms.shared.Fields.Models.Renderers;

namespace amiliur.figforms.web.blazor.Form.Components.Renderers;

public static class RenderMapping
{
    private static readonly Dictionary<Type, Type> Map = new()
    {
        {typeof(HiddenRenderer), typeof(OoriFormHiddenField)},
        {typeof(TextFieldRenderer), typeof(OoriFormTextField)},
        {typeof(NumericFieldRenderer), typeof(OoriFormNumericField<>)},
        {typeof(PickerRenderer), typeof(OoriDropdownPickerField<,>)}
    };

    public static Type GetRenderer(IFormField field)
    {
        var renderType = Map.GetValueOrDefault(field.FieldRenderer.GetType());
        if(renderType == null)
            throw new Exception($"No renderer found for {field.FieldRenderer.GetType().Name}");
        
        if (renderType.IsGenericType)
        {
            if (renderType.GetGenericArguments().Length == 1)
                return renderType.MakeGenericType(((IFormGenericsFieldRenderer) field.FieldRenderer).GenericType);


            if (IsPickerFormField(field))
            {
                return MakePickerFormFieldRenderer(field, renderType);
            }
        }

        return renderType;
    }

    private static Type MakePickerFormFieldRenderer(IFormField field, Type renderType)
    {
        var tValueType = ((IFormGenericsFieldRenderer) field.FieldRenderer)
            .GenericType
            .GetProperty(((IWithRelatedModel) field).ValueFieldName)
            ?.PropertyType;

        var tItemType = ((IFormGenericsFieldRenderer) field.FieldRenderer).GenericType;

        return tValueType != null
            ? renderType.MakeGenericType(tValueType, tItemType)
            : renderType.MakeGenericType(tItemType);
    }

    private static bool IsPickerFormField(IFormField field)
    {
        return field.GetType().Name == typeof(PickerFormFieldModel<>).Name;
    }
}