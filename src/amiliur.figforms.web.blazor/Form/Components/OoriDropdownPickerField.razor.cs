using amiliur.figforms.shared.Fields.Models;
using amiliur.figforms.shared.Fields.Models.Datasource;
using amiliur.figforms.web.blazor.Form.Services;
using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Form.Components;

public partial class OoriDropdownPickerField<TValue, TItem> : OoriComplextTypeFormFieldBase<TValue>
{
    [Inject] private RemoteDataFromAPIService RemoteDataService { get; set; }
    public List<TItem> Results { get; set; }
    // public SfDropDownList<TValue, TItem> dropDown { get; set; }
    private PickerFormFieldModel<TItem> PickerField => (PickerFormFieldModel<TItem>) this.FormField;
    // public OoriDynamicDialog AddNewElementWindow { get; set; }
    public Dictionary<string, object> ExtraParameters { get; set; } = new();

    [Parameter] public EventCallback<RelatedFieldToMustUpdateArgs> RelatedFieldToMustUpdate { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Results = await BindValues();
        // if (AddNewElementWindow != null)
        // {
        //     ExtraParameters[nameof(AddNewElementWindow.ControlInstance.OnSaveData)] = EventCallback.Factory.Create<SaveDataEventArgs>(this, ControlInstanceOnOnSaveData);
        //     ExtraParameters[nameof(AddNewElementWindow.ControlInstance.OnSaveDataSucceeded)] = EventCallback.Factory.Create<SavedDataEventArgs>(this, ControlInstanceOnOnSaveDataSucceeded);
        //     ExtraParameters[nameof(AddNewElementWindow.ControlInstance.OnSaveDataFailed)] = EventCallback.Factory.Create<SavedDataEventArgs>(this, ControlInstanceOnOnSaveDataFailed);
        // }
    }

    // private void ControlInstanceOnOnSaveDataFailed(SavedDataEventArgs args)
    // {
    // }
    //
    // private async void ControlInstanceOnOnSaveDataSucceeded(SavedDataEventArgs args)
    // {
    //     Results = await BindValues();
    //     await AddNewElementWindow.Hide();
    //     StateHasChanged();
    // }
    //
    // private void ControlInstanceOnOnSaveData(SaveDataEventArgs args)
    // {
    // }

    private async Task<List<TItem>> BindValues()
    {
        var dataSource = PickerField?.Datasource;
        if (dataSource == null)
            return new List<TItem>();

        if (dataSource.IsInlineData())
            return BindInlineValues(dataSource);
        return await BindRemoteValues(dataSource);
    }

    private async Task<List<TItem>> BindRemoteValues(IDatasource<TItem> dataSource)
    {
        var ds = (RemoteDatasource<TItem>) dataSource;
        return await RemoteDataService.GetAll<TItem>(ds);
    }

    private List<TItem> BindInlineValues(IDatasource<TItem> dataSource)
    {
        var ds = ((InlineDatasource<TItem>) dataSource);
        return ds.Objects ?? new List<TItem>();
    }

    private void DropDownOnValueSelect()
    {
    }

    // private async Task DropDownOnValueChange(ChangeEventArgs<TValue, TItem> arg)
    // {
    //     await OnValueChanged(arg.Value);
    //     await InvokeRelatedFieldToMustUpdate(arg.ItemData.GetPropertyValue(PickerField.TextFieldName));
    // }

    private async Task InvokeRelatedFieldToMustUpdate(object value)
    {
        await RelatedFieldToMustUpdate.InvokeAsync(new RelatedFieldToMustUpdateArgs() {Value = value, FieldToUpdate = PickerField.StoreSelectionTextInField});
    }

    // private async Task OnAddClick()
    // {
    //     await AddNewElementWindow.Show();
    // }
}