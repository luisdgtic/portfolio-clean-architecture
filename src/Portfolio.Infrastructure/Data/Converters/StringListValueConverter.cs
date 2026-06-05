using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Portfolio.Infrastructure.Data.Converters;

public class StringListValueConverter : ValueConverter<IReadOnlyList<string>, string>
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public StringListValueConverter()
        : base(
            v => Serialize(v),
            v => Deserialize(v))
    {
    }

    private static string Serialize(IReadOnlyList<string> values)
    {
        return values is null or { Count: 0 }
            ? "[]"
            : JsonSerializer.Serialize(values, Options);
    }

    private static IReadOnlyList<string> Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value == "[]")
            return Array.Empty<string>();

        try
        {
            return JsonSerializer.Deserialize<List<string>>(value, Options) ?? new List<string>();
        }
        catch
        {
            return Array.Empty<string>();
        }
    }
}
