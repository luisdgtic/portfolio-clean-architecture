using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;
using Portfolio.Infrastructure.Data.Repositories;
using Portfolio.Infrastructure.Services;

namespace Portfolio.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var useSqlite = configuration["UseSqlite"]?.ToLowerInvariant() == "true";

        services.AddDbContext<PortfolioDbContext>((sp, options) =>
        {
            if (useSqlite)
            {
                options.UseSqlite(
                    configuration.GetConnectionString("Sqlite"),
                    b => b.MigrationsAssembly(typeof(PortfolioDbContext).Assembly.FullName));
            }
            else
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("Postgres"),
                    b => b.MigrationsAssembly(typeof(PortfolioDbContext).Assembly.FullName));
            }

            options.EnableSensitiveDataLogging();
        });

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<PortfolioDbContext>());

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IExperienceRepository, ExperienceRepository>();
        services.AddScoped<IEducationRepository, EducationRepository>();
        services.AddScoped<ICertificationRepository, CertificationRepository>();
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<IContactMessageRepository, ContactMessageRepository>();

        services.AddScoped<IEmailService, NoOpEmailService>();

        services.AddHealthChecks()
            .AddDbContextCheck<PortfolioDbContext>(
                "database",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "db", "readiness" });

        return services;
    }
}
