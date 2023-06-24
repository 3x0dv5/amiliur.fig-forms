using amiliur.web.shared.Models.Generic;

namespace amiliur.figforms.shared.Models;

public class FormDataSearchInputModel : BaseSearchInputModel
{
    public FormDataSearchInputModel()
    {
    }

    public FormDataSearchInputModel(FormDefinition formDefinition, FilterExpr filterExpr)
    {
        FormDefinition = formDefinition;
        DataFilter = filterExpr;
    }

    public FormDefinition FormDefinition { get; set; } = null!;
    public FilterExpr DataFilter { get; set; } = null!;
}

public class FormDataSearchResultModel : BaseSearchResultModel
{
    public FormDataSearchInputModel FormDataSearchInputModel { get; set; } = null!;
    public List<BaseEditModel> Data { get; set; } = null!;

    public BaseEditModel SingleRecord()
    {
        if (Data.Count == 0)
            throw new Exception("No records found");
        if (Data.Count > 1)
            throw new Exception("More than one record found");
        return Data[0];
    }
}