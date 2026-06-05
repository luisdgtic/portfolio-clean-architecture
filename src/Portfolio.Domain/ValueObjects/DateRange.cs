namespace Portfolio.Domain.ValueObjects;

public sealed record DateRange
{
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; }

    public DateRange(DateTime startDate, DateTime? endDate = null)
    {
        StartDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
        EndDate = endDate.HasValue ? DateTime.SpecifyKind(endDate.Value, DateTimeKind.Utc) : null;

        if (EndDate.HasValue && EndDate.Value < StartDate)
            throw new ArgumentException("End date must be after start date.", nameof(endDate));
    }

    public bool IsCurrent => !EndDate.HasValue;

    public override string ToString()
    {
        var start = StartDate.ToString("MMM yyyy");
        var end = EndDate?.ToString("MMM yyyy") ?? "Present";
        return $"{start} - {end}";
    }
}
