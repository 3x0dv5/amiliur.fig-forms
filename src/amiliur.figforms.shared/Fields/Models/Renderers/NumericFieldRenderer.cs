namespace amiliur.figforms.shared.Fields.Models.Renderers;

public interface IFormGenericsFieldRenderer : IFormFieldRenderer
{
    public Type GenericType { get; set; }
}

public class NumericFieldRenderer : IFormGenericsFieldRenderer
{
    public Type GenericType { get; set; }

    public NumericFieldRenderer() : this(typeof(int))
    {
    }

    public NumericFieldRenderer(Type genericType)
    {
        GenericType = genericType;
    }
}