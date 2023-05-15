using amiliur.figforms.shared.Fields.Models.Datasource;
using amiliur.web.blazor.Services.AppState;
using amiliur.web.blazor.Services.Base;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace amiliur.figforms.web.blazor.Form.Services;

public class RemoteDataFromAPIService : ServerServiceBase
{
    public RemoteDataFromAPIService(HttpClient httpClient, AppStateService stateService, PageStateService pageStateService, IAccessTokenProvider accessTokenProvider) : base(httpClient, stateService, pageStateService, accessTokenProvider)
    {
    }

    protected override string SubPath => "not-used";

    public async Task<T> Get<T>(string url)
    {
        return await GetJson<T>(BuildAPIUrl(url));
    }

    public async Task<List<T>> GetAll<T>(RemoteDatasource<T> datasource)
    {
        return await GetJson<List<T>>(BuildAPIUrl(datasource.Url));
    }

    private string BuildAPIUrl(string url)
    {
        return $"{API}/{url}";
    }
}