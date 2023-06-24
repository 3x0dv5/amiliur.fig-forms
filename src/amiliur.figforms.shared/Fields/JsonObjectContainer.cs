using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using amiliur.figforms.shared.Validation;
using amiliur.web.shared.Json;
using JetBrains.Annotations;

namespace amiliur.figforms.shared.Fields;

public class JsonObjectContainer
{
    public string JsonContents { get; set; }
    public string ObjectType { get; set; }

    private object _data;

    public JsonObjectContainer()
    {
    }

    public JsonObjectContainer(object data)
    {
        _data = data;
        var objectTypeName = _data.GetType().FullName;
        var assemblyName = _data.GetType().Assembly.FullName?.Split(',')[0];
        JsonContents = JsonSerializer.Serialize(_data, new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                new GridColBaseJsonConverter()
            } 
        });
        ObjectType = $"{objectTypeName}, {assemblyName}";
    }

    public static JsonObjectContainer FromJson(string json)
    {
        return JsonSerializer.Deserialize<JsonObjectContainer>(json, new JsonSerializerOptions {PropertyNameCaseInsensitive = true, Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}});
    }

    public string ToJson([CanBeNull] JsonSerializerOptions options = null)
    {
        return JsonSerializer.Serialize(this, options);
    }

    public object ToObject()
    {
        var theType = Type.GetType(ObjectType);
        return theType != null ? JsonSerializer.Deserialize(JsonContents, theType, new JsonSerializerOptions {Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}}) : null;
    }

    public static List<FormFieldValidation> DeserializeListFromJsonToObjectList(string jsonContent)
    {
        using var j = JsonDocument.Parse(jsonContent);
        return j.RootElement.EnumerateArray().Select(elem => (FormFieldValidation) FromJson(elem.ToString()).ToObject()).ToList();
    }

    public string ToFriendlyJson()
    {
        var jsonContents = JsonSerializer
            .Serialize(
                _data,
                new JsonSerializerOptions
                {
                    WriteIndented = true, 
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                        new GridColBaseJsonConverter()
                    }
                }
            );
            
        return $"""
        //"ObjectType" => "{ObjectType}"
         
        "JsonContents" =>  {jsonContents} 
        """;
    }
}