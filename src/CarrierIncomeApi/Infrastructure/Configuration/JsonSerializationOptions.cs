using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace CarrierIncomeApi.Infrastructure.Configuration;

public static class JsonSerializationOptions
{
    public static Action<JsonOptions> Default = (JsonOptions options) =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    };

    public static Action<JsonOptions> Development = (JsonOptions options) =>
    {
        Default(options);
        options.JsonSerializerOptions.WriteIndented = true;
    };
}
