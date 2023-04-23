using amiliur.figforms.shared.Attributes.Datagrid.Enums;
using amiliur.figforms.shared.Attributes.Datagrid.Models.ColumnTypes.ColumnConditions;
using amiliur.figforms.shared.Attributes.Datagrid.Models.ColumnTypes.Exceptions;
using amiliur.shared.Extensions;

namespace amiliur.figforms.shared.Attributes.Datagrid.Models.ColumnTypes;

public class CompoundCol : GridColBase
{
    public override TypeOfColumn TypeOfColumn => TypeOfColumn.Compound;
    public List<GridColBase> ColControls { get; set; } = new();
    public List<ICondition> ColConditions { get; set; } = new();

    public CompoundCol(string field, string name) : base(field, name)
    {
        HeaderText = "";
    }

    public CompoundCol()
    {
        HeaderText = "";
    }

    private void AddControl(GridColBase control)
    {
        if (ColControls.Any(m => m.ControlName == control.ControlName))
            throw new DuplicateControlException(control.ControlName);

        ColControls.Add(control);
    }

    private void AddCondition(ICondition condition)
    {
        if (ColControls.All(m => m.ControlName != condition.ControlName))
            throw new MissingControlException(condition.ControlName);

        ColConditions.Add(condition);
    }

    public static CompoundCol Instance(string field, List<GridColBase> controls, List<ICondition> conditions)
    {
        string renderingName = StringUtils.ToSnakeCase($"col_comp_{field}");

        var compoundCol = new CompoundCol(field, renderingName);
        foreach (var cont in controls)
        {
            compoundCol.AddControl(cont);
        }

        foreach (var cond in conditions)
        {
            compoundCol.AddCondition(cond);
        }

        return compoundCol;
    }

    public GridColBase GetControl(string controlName)
    {
        return ColControls.First(m => m.ControlName == controlName);
    }
}
