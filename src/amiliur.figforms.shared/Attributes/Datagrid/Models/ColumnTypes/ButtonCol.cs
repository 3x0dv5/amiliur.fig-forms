using amiliur.figforms.shared.Attributes.Datagrid.Enums;

namespace amiliur.figforms.shared.Attributes.Datagrid.Models.ColumnTypes;

public class ButtonCol : GridColBase
{
    public override TypeOfColumn TypeOfColumn => TypeOfColumn.Button;
    public TypeOfButton TypeOfButton { get; set; } = TypeOfButton.Edit;
    public override TextAlign? TextAlign { get; set; } = Enums.TextAlign.Center;
    public override string Width { get; set; } = "50";
    public string? UrlFormat { get; set; }
    public string? Text { get; set; }
    public string? DialogMessageTextFormat { get; set; }

    public ButtonCol(string field, string name) : base(field, name)
    {
    }
}