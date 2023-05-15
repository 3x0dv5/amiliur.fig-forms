using amiliur.shared.Json;
using amiliur.web.shared.Models;

namespace amiliur.figforms.shared.Fields.Models.Datasource;

public interface IDatasource<T> : ISerializableModel
{
    bool IsInlineData();
}

public class InlineDatasource<T> : IDatasource<T>
{
    public List<T> Objects { get; set; }

    public bool IsInlineData()
    {
        return true;
    }
}

public class RemoteDatasource<T> : IDatasource<T>
{
    public bool IsAPICall { get; set; } = true;
    public string Url { get; set; }

    public RemoteDatasource()
    {
    }

    public RemoteDatasource(string url)
    {
        Url = url;
    }

    public bool IsInlineData()
    {
        return false;
    }
}

public class RemoteValueTextDatasource : RemoteDatasource<ValueTextModel>
{
    public RemoteValueTextDatasource()
    {
    }
    public RemoteValueTextDatasource(string url) : base($"form-data/ativos?e={url}")
    {
    }
}