using System.Runtime.Serialization;

namespace amiliur.figforms.shared.Fields;

public enum CalendarView
{
    /// <summary>Specifies the Month view of the calendar.</summary>
    [EnumMember(Value = "Month")] Month,
    /// <summary>Specifies the Year view of the calendar.</summary>
    [EnumMember(Value = "Year")] Year,
    /// <summary>Specifies the Decade view of the calendar.</summary>
    [EnumMember(Value = "Decade")] Decade,
}