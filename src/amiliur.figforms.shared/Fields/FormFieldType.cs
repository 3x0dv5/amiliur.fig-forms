using System.Runtime.Serialization;

namespace amiliur.figforms.shared.Fields;

public enum FormFieldType
{
    [EnumMember(Value="text")]
    Text,
    [EnumMember(Value="char")]
    Char,
    [EnumMember(Value="numeric")]
    Numeric,
    [EnumMember(Value="bool")]
    Boolean,
    [EnumMember(Value="date")]
    Date,
    [EnumMember(Value= "picker")]
    Picker
}