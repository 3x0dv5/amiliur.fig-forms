using System.Text.Json.Serialization;
using amiliur.figforms.shared.Fields.Models;
using amiliur.shared.Json;
using amiliur.web.shared.Forms;
using Serilog;

namespace amiliur.figforms.shared;

public class FormDefinition : ISerializableModel
{
    public FormDefinition()
    {
    }

    public FormDefinition(string formContext, string formModule, string formCode, string formName, FormMode formMode, string formTitle)
    {
        FormContext = formContext;
        FormModule = formModule;
        FormCode = formCode;
        FormName = formName;
        FormMode = formMode;
        FormTitle = formTitle;
    }

    public string FormContext { get; set; } = null!;
    public string FormModule { get; set; } = null!;
    public string FormCode { get; set; } = null!;
    public string FormName { get; set; } = null!;
    public string FormTitle { get; set; } = null!;
    public FormMode FormMode { get; set; }


    public string? FormDescription { get; set; }
    public List<FormDefinitionRow> Rows { get; set; } = new();
    public bool SaveOnClick { get; set; } = true;
    public string DataTypeName { get; set; } = null!;

    [JsonIgnore] public IEnumerable<BaseFormFieldModel> Fields => Rows.SelectMany(m => m.Fields);

    [JsonIgnore] public string Id => $"{FormContext}.{FormModule}.{FormCode}.{FormMode}";

    public DataSource LoadDataSource { get; set; } = null!;

    public void AddRow(BaseFormFieldModel[] fields)
    {
        var cellBootstrapSpan = 12 / fields.Count(m => m.IsVisible());

        var newRow = new FormDefinitionRow();
        foreach (var field in fields)
        {
            var cell = new FormDefinitionCell
            {
                Visible = field.IsVisible(),
                Fields = new List<BaseFormFieldModel> {field},
                ColumnBootstrapSpan = cellBootstrapSpan
            };
            newRow.Cells.Add(cell);
        }

        Rows.Add(newRow);
    }

    public void AddField(BaseFormFieldModel field, int? cellBootstrapSpan = null, int? rowNumber = null)
    {
        if (rowNumber == null) Rows.Add(new FormDefinitionRow());

        rowNumber = Rows.Count;
        var row = Rows[rowNumber.Value - 1];
        var fieldsCount = row.Fields.Count(m => m.IsVisible());
        if (fieldsCount == 0) fieldsCount = 1;
        cellBootstrapSpan = cellBootstrapSpan ?? 12 / fieldsCount;

        row.Cells.Add(new FormDefinitionCell
        {
            ColumnBootstrapSpan = cellBootstrapSpan,
            Fields = new List<BaseFormFieldModel> {field},
            Visible = field.IsVisible()
        });
    }
}

public class FormDefinitionRow : ISerializableModel
{
    public List<FormDefinitionCell> Cells { get; set; } = new();

    [JsonIgnore]
    public IEnumerable<BaseFormFieldModel> Fields
    {
        get { return Cells.SelectMany(m => m.Fields); }
    }
}

public class FormDefinitionCell : ISerializableModel
{
    public FormDefinitionCell()
    {
    }

    public FormDefinitionCell(List<BaseFormFieldModel> fields)
    {
        Fields = fields;
    }

    public List<BaseFormFieldModel> Fields { get; set; } = new();

    public int? ColumnBootstrapSpan { get; set; } = 6;
    public bool Visible { get; set; }
}

public abstract class DataSource : ISerializableModel
{
    public abstract string AsUrl();
}

public class ApiDataSource : DataSource
{
    public string ApiUrl { get; set; } = null!;
    public string ApiMethod { get; set; } = null!;

    public override string AsUrl()
    {
        Log.Debug("ApiUrl: {ApiUrl}", ApiUrl);
        Log.Debug("ApiMethod: {ApiMethod}", ApiMethod);
        return $"{ApiUrl}/{ApiMethod}";
    }
}