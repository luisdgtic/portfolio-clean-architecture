using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Portfolio.Infrastructure.Data.Converters;

public class JsonArrayValueConverter<T> : ValueConverter<IReadOnlyList<T>, string>
    where T : class
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public JsonArrayValueConverter()
        : base(
            v => Serialize(v),
            v => Deserialize(v))
    {
    }

    private static string Serialize(IReadOnlyList<T> values)
    {
        return values is null or { Count: 0 }
            ? "[]"
            : JsonSerializer.Serialize(values, Options);
    }

    private static IReadOnlyList<T> Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value == "[]")
            return Array.Empty<T>();

        try
        {
            return JsonSerializer.Deserialize<List<T>>(value, Options) ?? new List<T>();
        }
        catch
        {
            return Array.Empty<T>();
        }
    }
}
