using System.ComponentModel.DataAnnotations;
using amiliur.figforms.shared.Attributes.Interfaces;

namespace amiliur.figforms.shared.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class RequiredFieldAttribute: RequiredAttribute, IFormFieldAttribute
{
    public FormMode[] FormModes { get; set; } = null!;
}