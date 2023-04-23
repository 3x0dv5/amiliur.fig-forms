using System.Runtime.Serialization;

namespace amiliur.figforms.shared.Attributes.Datagrid.Enums;

public enum OoriTextClipMode
{
    [EnumMember(Value = "Clip")] 
    Clip,

    [EnumMember(Value = "Ellipsis")]
    Ellipsis,

    [EnumMember(Value = "EllipsisWithTooltip")]
    EllipsisWithTooltip,
}