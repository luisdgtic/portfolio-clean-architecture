using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace Portfolio.WebApi.Extensions;

public static class HealthCheckExtensions
{
    public static IEndpointRouteBuilder MapHealthCheckEndpoint(this IEndpointRouteBuilder endpoints, string pattern = "/api/health")
    {
        endpoints.MapGet(pattern, async (HealthCheckService healthCheckService) =>
        {
            var report = await healthCheckService.CheckHealthAsync();

            var response = new
            {
                Status = report.Status.ToString(),
                Duration = report.TotalDuration,
                Entries = report.Entries.Select(e => new
                {
                    Name = e.Key,
                    Status = e.Value.Status.ToString(),
                    Description = e.Value.Description,
                    Duration = e.Value.Duration,
                    Exception = e.Value.Exception?.Message
                })
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            var statusCode = report.Status == HealthStatus.Healthy ? 200 : 503;

            return Results.Text(json, "application/json", statusCode: statusCode);
        });

        return endpoints;
    }
}
