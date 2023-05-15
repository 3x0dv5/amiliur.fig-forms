using amiliur.web.shared.Models;

namespace amiliur.figforms.shared.Fields.Models.Renderers;

public class PickerRenderer : IFormGenericsFieldRenderer
{
    public Type GenericType { get; set; }

    public PickerRenderer(): this(typeof(ValueTextModel))
    {
        
    }

    public PickerRenderer(Type genericType)
    {
        GenericType = genericType;
    }
}