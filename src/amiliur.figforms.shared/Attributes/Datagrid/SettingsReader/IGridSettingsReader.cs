using amiliur.figforms.shared.Attributes.Datagrid.Models;

namespace amiliur.figforms.shared.Attributes.Datagrid.SettingsReader;

public interface IGridSettingsReader
{
    public DataGridSettings Read(Type tValueObj);
}