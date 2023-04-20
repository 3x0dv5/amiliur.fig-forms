using amiliur.figforms.shared.Attributes.Interfaces;

namespace amiliur.figforms.shared.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class GenerateSlugAttribute : Attribute, IFormFieldAttribute
{
    public FormMode[] FormModes { get; set; } = null!;
    
    public string SourceField { get; set; } = null!;
}