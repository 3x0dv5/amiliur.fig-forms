using amiliur.figforms.shared;
using amiliur.web.blazor.Services.AppState;
using amiliur.web.blazor.Services.Base;
using amiliur.web.shared.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Serilog;

namespace amiliur.figforms.web.blazor.Form.Services;

public class FormDefinitionService : ServerServiceBase
{
    private readonly Dictionary<(string, string, string, FormMode), FormDefinition> _cache = new();


    public FormDefinitionService(HttpClient httpClient, AppStateService stateService, PageStateService pageStateService, IAccessTokenProvider accessTokenProvider) : base(httpClient, stateService, pageStateService, accessTokenProvider)
    {
    }

    protected override string SubPath => "form-definition";

    public async Task<FormDefinition?> GetFormDefinition(string formCode, string formContext, string formModule, FormMode formMode)
    {
        Log.Information("FormDefinitionService.GetFormDefinition({formCode}, {formModule}, {formContext}, {formMode})",
            formCode, formModule, formContext, formMode);
        Log.Debug("FormDefinitionService.GetFormDefinition({formCode}, {formModule}, {formContext}, {formMode})",
            formCode, formModule, formContext, formMode);


        if (_cache.TryGetValue((formCode, formContext, formModule, formMode), out var formDefinition))
        {
            Log.Debug("FormDefinitionService.GetFormDefinition({formCode}, {formContext}, {formModule}, {formMode}) - from cache: {formDefinition}",
                formCode,
                formContext,
                formModule,
                formMode,
                formDefinition);
            return formDefinition;
        }

        Log.Debug("Not found in cache");
        
        formDefinition = await Get<FormDefinition>("", $"get/{formContext}/{formModule}/{formCode}/{formMode.ToString().ToLower()}");
        _cache[(formCode, formContext, formModule, formMode)] = formDefinition;
        return formDefinition;
    }
}