namespace amiliur.figforms.shared.Attributes.Datagrid.Models.ColumnTypes.Exceptions;

public class MissingControlException : Exception
{
    public MissingControlException(string controlName): base(string.Format(ExceptionMessages.MissingControlExceptionMessageFormat, controlName))
    {
    }
}