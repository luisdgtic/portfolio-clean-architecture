namespace Portfolio.Domain.ValueObjects;

public sealed record Url
{
    public string Value { get; }

    public Url(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("URL cannot be empty.", nameof(value));

        if (!Uri.TryCreate(value, UriKind.Absolute, out var uri))
            throw new ArgumentException($"'{value}' is not a valid URL.", nameof(value));

        if (uri.Scheme != "http" && uri.Scheme != "https")
            throw new ArgumentException($"'{value}' must be an HTTP or HTTPS URL.", nameof(value));

        Value = uri.ToString().TrimEnd('/');
    }

    public override string ToString() => Value;

    public static implicit operator string(Url url) => url.Value;
    public static explicit operator Url(string value) => new(value);
}
