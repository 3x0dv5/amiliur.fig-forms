using amiliur.figforms.shared;
using amiliur.figforms.shared.Fields.Models;
using amiliur.figforms.web.blazor.Form.Components;
using amiliur.figforms.web.blazor.Form.Components.Args;
using amiliur.figforms.web.blazor.Form.Components.Renderers;
using amiliur.figforms.web.blazor.Form.Services;
using amiliur.shared.Json;
using amiliur.shared.Reflection;
using amiliur.web.shared.Forms;
using amiliur.web.shared.Models.Generic;
using amiliur.web.shared.Models.Results;
using amiliur.web.shared.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Serilog;
using TypeUtils = amiliur.shared.Reflection.TypeUtils;

namespace amiliur.figforms.web.blazor.Form;

public partial class FigForm : ComponentBase, IOoriFieldEventHandlers
{
    #region Parameters

    [Parameter] public EventCallback<InputChangedArgs> InputChanged { get; set; }
    [Parameter] public EventCallback<FormDefinitionLoadedArgs> FormDefinitionLoaded { get; set; }
    [Parameter] public EventCallback<FormDataLoadedArgs> FormDataLoaded { get; set; }
    [Parameter] public EventCallback<FormDataSavedArgs> OnSaveFailed { get; set; }
    [Parameter] public EventCallback<FormDataSavedArgs> OnSaveSuccess { get; set; }


    [Parameter] public string FormContext { get; set; } = null!;
    [Parameter] public string FormModule { get; set; } = null!;
    [Parameter] public string FormCode { get; set; } = null!;
    [Parameter] public FormMode FormMode { get; set; } = FormMode.View;
    [Parameter] public FilterExpr? FilterExpr { get; set; }

    #endregion

    #region Injected Services

    [Inject] public FormDataService DataService { get; set; } = null!;
    [Inject] public FormDefinitionService FormDefinitionService { get; set; } = null!;

    #endregion

    #region Properties and attributes

    public BaseEditModel? Input { get; set; }
    public bool InEditMode => Input != null && Input.IsEdit;
    private IEnumerable<BaseFormFieldModel> Fields => FormDefinition?.Fields ?? Array.Empty<BaseFormFieldModel>();
    private string? ErrorMessage { get; set; } = null!;

    public FormDefinition? FormDefinition { get; set; }

    private string _loadedFormContext = null!;
    private string _loadedFormCode = null!;
    private string _loadedFormModule = null!;
    private FormMode? _loadedFormMode = null!;
    public bool IsDataLoaded { get; private set; }
    private Dictionary<string, OoriFormField> FieldComponents { get; set; } = new();


    private readonly string _formComponentId = Guid.NewGuid().ToString("N");

    private EditContext EditContext { get; set; } = null!;

    #endregion

    #region Lifecycle

    protected override async Task OnInitializedAsync()
    {
        Log.Debug("OnInitializedAsync:LoadFormDefinition:{hashCode} : {formid}", GetHashCode(), _formComponentId);
        await LoadFormDefinition();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (IsDifferentForm())
        {
            Log.Debug("OnParametersSetAsync:LoadFormDefinition: {hashCode}: {formId}, {formCode} vs {loadedFormCode}, {formContext} vs {loadedFormContext}",
                GetHashCode(), _formComponentId, FormCode, _loadedFormCode, FormContext, _loadedFormContext);
            IsDataLoaded = false;
            await LoadFormDefinition();
        }
    }

    private bool IsDifferentForm()
    {
        return
            FormContext != _loadedFormContext
            || FormCode != _loadedFormCode
            || FormModule != _loadedFormModule
            || FormMode != _loadedFormMode;
    }

    #endregion

    public Task OnFieldChanged(IFormField field, FieldChangedArgs args)
    {
        if (Input != null)
            Input.SetPropertyValue(args.PropertyName, args.NewValue);
        return Task.CompletedTask;
    }

    private async Task LoadFormDefinition()
    {
        Log.Debug($"Loading form definition:Loading form definition: {FormDefinitionService.GetHashCode()} : {_formComponentId}");
        FormDefinition = await FormDefinitionService.GetFormDefinition(FormCode, FormContext, FormModule, FormMode);
        _loadedFormContext = FormContext;
        _loadedFormCode = FormCode;
        _loadedFormModule = FormModule;
        _loadedFormMode = FormMode;

        LoadFieldRenderers();

        await InvokeFormDefinitionLoaded();

        if (FormDefinition == null)
            return;
        await ResetData();
    }

    private void LoadFieldRenderers()
    {
        FieldComponents = new Dictionary<string, OoriFormField>();
        foreach (var field in Fields)
        {
            if (field.FieldRenderer == null)
                continue;

            var rendererType = RenderMapping.GetRenderer(field);

            var renderer = (OoriFormField) Activator.CreateInstance(rendererType)!;
            FieldComponents.Add(field.FieldName, renderer);
        }
    }

    private async Task InvokeFormDefinitionLoaded()
    {
        if (FormDefinition != null)
            await FormDefinitionLoaded.InvokeAsync(new FormDefinitionLoadedArgs {FormDefinition = FormDefinition});
    }

    public async Task ResetData()
    {
        if (FormDefinition == null)
            return;

        var type = TypeUtils.FindTypeByName(FormDefinition.DataTypeName.Split(',')[0].Trim(), new List<string> {FormDefinition.DataTypeName.Split(',')[1].Trim()});
        if (type == null)
        {
            throw new Exception($"Could not find type {FormDefinition.DataTypeName}");
        }

        if (FilterExpr != null)
        {
            var result = await DataService.GetData(FormDefinition, FilterExpr);
            Input = result != null ? result.SingleRecord() : CreateEmptyInstanceOfEditModel(type);
        }
        else
            Input = CreateEmptyInstanceOfEditModel(type);

        if (Input != null)
            EditContext = new EditContext(Input);
    }

    private static BaseEditModel CreateEmptyInstanceOfEditModel(Type type)
    {
        var result = (BaseEditModel?) Activator.CreateInstance(type);
        if (result == null)
            throw new Exception($"Could not create instance of {type.FullName}");
        return result;
    }

    private async Task InvokeFormDataLoaded()
    {
        await FormDataLoaded.InvokeAsync(new FormDataLoadedArgs {FormDefinition = FormDefinition, Data = Input});
        IsDataLoaded = true;
    }


    private IDictionary<string, object> FieldToParameters(IFormField field)
    {
        var fieldParameters = ExplodeFieldParameters(field) ?? new Dictionary<string, object>();
        var events = CustomEventsInField(field);

        if (events.Any())
            Log.Debug("events of {0}: {1}", field.FieldName, events.Select(e => e.Key).ToList().ToJson());
        var fieldAsParameters = events
            .Concat(fieldParameters)
            .Concat(
                new Dictionary<string, object>
                {
                    {"FormField", field},
                    {"FId", $"{FormDefinition.Id}-{field.FieldName}"},
                    {"FValue", Input.GetPropertyValue(field.FieldName)},
                    {"FieldEventHandlers", this},
                    {"FormInputModel", Input}
                })
            .ToDictionary(x => x.Key, x => x.Value);

        return fieldAsParameters;
    }


    private Dictionary<string, object> CustomEventsInField(IFormField field)
    {
        var events = new Dictionary<string, object>
        {
            {"RelatedFieldToMustUpdate", EventCallback.Factory.Create<RelatedFieldToMustUpdateArgs>(this, OnRelatedFieldToMustUpdate)}
        };

        var eventProperties = GetDynamicComponentType(field)
            .GetEventCallbackProperties()
            .Select(p => p.Name)
            .ToList();

        Log.Logger.Debug("Envent properties: {0}", eventProperties.Count);

        foreach (var ep in eventProperties)
        {
            Log.Debug("Event property: {0}", ep);
        }

        var evtInFilters = events
            .Where(e => eventProperties.Contains(e.Key))
            .ToDictionary(x => x.Key, x => x.Value);

        Log.Debug("Evt In filters: {0}", evtInFilters.Count);
        Log.Debug("Evt json: {0}", evtInFilters.ToJson());
        return evtInFilters;
    }

    private Task OnRelatedFieldToMustUpdate(RelatedFieldToMustUpdateArgs args)
    {
        Input.SetPropertyValue(args.FieldToUpdate, args.Value);
        return Task.CompletedTask;
    }

    private Dictionary<string, object> ExplodeFieldParameters(IFormField field)
    {
        var rendererProperties = GetDynamicComponentType(field)
            .GetProperties()
            .Select(m => m.Name).ToList();

        return field.GetProperties()
            .Where(p => rendererProperties.Contains(p.Name))
            .Select(p => new
            {
                p.Name,
                Value = field.GetPropertyValueNonNull(p.Name)
            })
            .ToDictionary(x => x.Name, x => x.Value);
    }

    private Type GetDynamicComponentType(IFormField field)
    {
        Log.Debug($"GetDynamicComponentType called for {field.FieldName}");
        var renderer = RenderMapping.GetRenderer(field);
        Log.Debug("Renderer: {0}", TypeUtils.GetFullTypeName(renderer));
        return renderer;
    }

    private OoriFormField GetDynamicComponent(IFormField field)
    {
        return FieldComponents[field.FieldName];
    }

    public BaseEditModel? GetFormValues()
    {
        return Input;
    }

    public bool HasValidData()
    {
        return EditContext.Validate();
    }

    private string ColFromCell(FormDefinitionCell cell)
    {
        return cell.ColumnBootstrapSpan == null
            ? "col"
            : $"col-{cell.ColumnBootstrapSpan}";
    }

    public async Task SaveData()
    {
        ErrorMessage = string.Empty;
        if (HasValidData() && FormDefinition != null)
        {
            var model = GetFormValues();
            if (model == null)
                return;

            var result = await DataService.SaveFormData<SaveBaseResult>(FormDefinition, model);
            var savedArgs = new FormDataSavedArgs
            {
                Data = model,
                FormDefinition = FormDefinition,
                Result = result
            };
            if (result.Success == false)
            {
                ErrorMessage = result.ErrorMessage;
                await OnSaveFailed.InvokeAsync(savedArgs).ConfigureAwait(false);
            }
            else
            {
                await OnSaveSuccess.InvokeAsync(savedArgs).ConfigureAwait(false);
            }

            Log.Debug("Save result: {0}", result.ToJson());
        }
    }
}