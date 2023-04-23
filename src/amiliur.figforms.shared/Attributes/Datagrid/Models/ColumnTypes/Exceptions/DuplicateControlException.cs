namespace amiliur.figforms.shared.Attributes.Datagrid.Models.ColumnTypes.Exceptions;

public class DuplicateControlException : Exception
{
    public DuplicateControlException(string controlName) : base(string.Format(ExceptionMessages.DuplicateControlExceptionMessageFormat, controlName))
    {
    }
}