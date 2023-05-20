using amiliur.figforms.shared;
using amiliur.figforms.shared.Models;
using amiliur.web.blazor.Services.AppState;
using amiliur.web.blazor.Services.Base;
using amiliur.web.shared.Models.Generic;
using amiliur.web.shared.Models.Results;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace amiliur.figforms.web.blazor.Form.Services;

public class FormDataService : ServerServiceBase
{
    public FormDataService(HttpClient httpClient, AppStateService stateService, PageStateService pageStateService, IAccessTokenProvider accessTokenProvider) : base(httpClient, stateService, pageStateService, accessTokenProvider)
    {
    }

    protected override string SubPath => "";

    public async Task<SaveBaseResult?> SaveData(BaseEditModel values, FormDefinition formDefinition)
    {
        return await Save<FormDataSaveContainer, SaveBaseResult>(
            new FormDataSaveContainer
            {
                Data = values,
                FormDefinition = formDefinition
            },
            ActionUrl($"save/{formDefinition.FormContext}/{formDefinition.FormCode}"));
    }

    public async Task<FormDataSearchResultModel?> GetData(FormDefinition formDefinition, FilterExpr filterExpr)
    {
        var request = new FormDataSearchInputModel(formDefinition, filterExpr);

        var response = await PostRetrieveAsync<FormDataSearchInputModel, FormDataSearchResultModel>(request, formDefinition.LoadDataSource.AsUrl());
        return response;
    }

    public async Task<T> SaveFormData<T>(FormDefinition formDefinition,  BaseEditModel model) where T : class
    {
        var request = new FormDataSaveContainer
        {
            Data = model,
            FormDefinition = formDefinition
        };
        
        ClearCache();
        var response = await PostRetrieveAsync<FormDataSaveContainer, T>(request, formDefinition.SaveDataSource.AsUrl());
        
        return response;
    }
}