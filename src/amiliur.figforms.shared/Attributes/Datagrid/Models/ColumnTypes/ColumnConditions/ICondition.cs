namespace amiliur.figforms.shared.Attributes.Datagrid.Models.ColumnTypes.ColumnConditions;

public interface ICondition
{
    string ControlName { get; init; }
    bool Hit(object data);
}
