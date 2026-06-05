using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Infrastructure.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(PortfolioDbContext context, ILogger logger)
    {
        if (await context.Profiles.IgnoreQueryFilters().AnyAsync())
        {
            logger.LogInformation("Database already seeded. Skipping.");
            return;
        }

        logger.LogInformation("Seeding database...");

        var profile = new Profile
        {
            Id = Guid.NewGuid(),
            FullName = "Luis Antonio C\u00e1rdenas Montejano",
            Title = "Full-Stack .NET Developer",
            Summary = "Software Engineer focused on building web and desktop applications with Clean Architecture, ASP.NET MVC, Blazor, and WPF. Passionate about solving real-world problems, learning new technologies, and delivering innovative solutions with tangible impact.",
            PhotoUrl = "https://avatars.githubusercontent.com/u/placeholder",
            ResumeUrl = "/CV_Luis_Antonio_Cardenas_Montejano_EN.pdf",
            Email = "luisdgtic@gmail.com",
            Phone = null,
            Location = "Colima, Colima, M\u00e9xico",
            LinkedInUrl = "https://www.linkedin.com/in/luis-cardenas-dev",
            GitHubUrl = "https://github.com/luisdgtic",
            TwitterUrl = null,
            WebsiteUrl = null,
            CreatedAt = DateTime.UtcNow
        };
        context.Profiles.Add(profile);

        var projects = new[]
        {
            new Project
            {
                Id = Guid.NewGuid(),
                Title = "Pre-Registration & Ecological Tax System",
                Description = "Public-facing platform managing complex fiscal business rules and high-concurrency data persistence. Designed and deployed to handle tax pre-registration workflows for government agencies with real-time validation and reporting.",
                ImageUrl = "https://placehold.co/800x400/0ea5e9/white?text=Tax+System",
                GitHubUrl = null,
                LiveUrl = null,
                TechStack = new List<string> { "ASP.NET MVC 5", ".NET Framework 4.8", "SQL Server", "JavaScript", "Clean Architecture" },
                IsFeatured = true,
                SortOrder = 1,
                CreatedAt = DateTime.UtcNow
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Title = "Institutional Correspondence System",
                Description = "Comprehensive document management platform with Role-Based Access Control (RBAC), dynamic operational modules, and secure document upload/management pipelines. Supports multi-department workflows with audit trails.",
                ImageUrl = "https://placehold.co/800x400/7c3aed/white?text=Correspondence+System",
                GitHubUrl = null,
                LiveUrl = null,
                TechStack = new List<string> { "ASP.NET MVC 5", "SQL Server", "MySQL", "JavaScript", "jQuery", "Clean Architecture" },
                IsFeatured = true,
                SortOrder = 2,
                CreatedAt = DateTime.UtcNow
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Title = "Responsive Blazor Website",
                Description = "Built a responsive, client-facing website using Blazor, HTML5, CSS3, JavaScript, and Syncfusion components. Managed end-to-end development from client requirement gathering to deployment.",
                ImageUrl = "https://placehold.co/800x400/059669/white?text=Blazor+Website",
                GitHubUrl = null,
                LiveUrl = null,
                TechStack = new List<string> { "Blazor", "HTML5", "CSS3", "JavaScript", "Syncfusion" },
                IsFeatured = true,
                SortOrder = 3,
                CreatedAt = DateTime.UtcNow
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Title = "Enterprise Desktop Application (WPF)",
                Description = "Created a desktop application with WPF following MVVM best practices, integrating Syncfusion and DevExpress components. Designed user interfaces for handling and visualizing large volumes of institutional data.",
                ImageUrl = "https://placehold.co/800x400/f59e0b/white?text=WPF+Application",
                GitHubUrl = null,
                LiveUrl = null,
                TechStack = new List<string> { "WPF", "MVVM", "DevExpress", "Syncfusion", "SQL Server" },
                IsFeatured = false,
                SortOrder = 4,
                CreatedAt = DateTime.UtcNow
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Title = "Professional Portfolio Web App",
                Description = "Full-stack portfolio built with .NET 9 Clean Architecture, CQRS with MediatR, PostgreSQL, and a React SPA with Tailwind CSS. Dockerized and deployed on Render with CI/CD via GitHub Actions.",
                ImageUrl = "https://placehold.co/800x400/dc2626/white?text=Portfolio+App",
                GitHubUrl = "https://github.com/luisdgtic/portfolio",
                LiveUrl = null,
                TechStack = new List<string> { ".NET 9", "React", "TypeScript", "PostgreSQL", "Docker", "Tailwind CSS", "Clean Architecture" },
                IsFeatured = true,
                SortOrder = 5,
                CreatedAt = DateTime.UtcNow
            }
        };
        context.Projects.AddRange(projects);

        var skills = new[]
        {
            new Skill { Id = Guid.NewGuid(), Name = "C# / .NET", Category = SkillCategory.Backend, Proficiency = 95, SortOrder = 1, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "ASP.NET MVC 5", Category = SkillCategory.Backend, Proficiency = 90, SortOrder = 2, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "ASP.NET Core Web API", Category = SkillCategory.Backend, Proficiency = 85, SortOrder = 3, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "Blazor", Category = SkillCategory.Backend, Proficiency = 78, SortOrder = 4, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "WPF / MVVM", Category = SkillCategory.Backend, Proficiency = 82, SortOrder = 5, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "JavaScript (ES6+)", Category = SkillCategory.Frontend, Proficiency = 80, SortOrder = 6, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "jQuery", Category = SkillCategory.Frontend, Proficiency = 78, SortOrder = 7, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "React / TypeScript", Category = SkillCategory.Frontend, Proficiency = 72, SortOrder = 8, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "Tailwind CSS", Category = SkillCategory.Frontend, Proficiency = 70, SortOrder = 9, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "SQL Server", Category = SkillCategory.Database, Proficiency = 90, SortOrder = 10, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "MySQL", Category = SkillCategory.Database, Proficiency = 82, SortOrder = 11, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "PostgreSQL", Category = SkillCategory.Database, Proficiency = 75, SortOrder = 12, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "Git / GitLab", Category = SkillCategory.DevOps, Proficiency = 90, SortOrder = 13, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "Docker", Category = SkillCategory.DevOps, Proficiency = 72, SortOrder = 14, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "Azure DevOps", Category = SkillCategory.Cloud, Proficiency = 68, SortOrder = 15, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "Clean Architecture", Category = SkillCategory.Other, Proficiency = 85, SortOrder = 16, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "Dependency Injection", Category = SkillCategory.Other, Proficiency = 90, SortOrder = 17, CreatedAt = DateTime.UtcNow },
            new Skill { Id = Guid.NewGuid(), Name = "Entity Framework Core", Category = SkillCategory.Backend, Proficiency = 80, SortOrder = 18, CreatedAt = DateTime.UtcNow }
        };
        context.Skills.AddRange(skills);

        var experiences = new[]
        {
            new Experience
            {
                Id = Guid.NewGuid(),
                Company = "ICSIC",
                Position = "Software Developer",
                Description = "Designed and deployed institutional systems using Clean Architecture in ASP.NET MVC 5 and .NET Framework 4.8. Built a public-facing tax pre-registration platform with complex fiscal business rules and high-concurrency data persistence. Implemented an institutional correspondence system with comprehensive RBAC, dynamic operational modules, and secure document upload pipelines. Managed code repositories, branching strategies, and deployments through Git and GitLab.",
                Period = new DateRange(new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Utc)),
                Technologies = new List<string> { "ASP.NET MVC 5", ".NET Framework 4.8", "SQL Server", "MySQL", "JavaScript", "jQuery", "Git", "GitLab", "Clean Architecture" },
                CompanyUrl = null,
                SortOrder = 1,
                CreatedAt = DateTime.UtcNow
            },
            new Experience
            {
                Id = Guid.NewGuid(),
                Company = "Freelance",
                Position = "Full-Stack Developer",
                Description = "Built a responsive website using Blazor, HTML5, CSS3, JavaScript, and Syncfusion components. Created a desktop application with WPF, integrating Syncfusion and DevExpress controls following MVVM best practices. Gathered client requirements through interviews and managed the end-to-end development process from conception to deployment.",
                Period = new DateRange(new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Utc)),
                Technologies = new List<string> { "Blazor", "WPF", "Syncfusion", "DevExpress", "MVVM", "HTML5", "CSS3", "JavaScript" },
                SortOrder = 2,
                CreatedAt = DateTime.UtcNow
            },
            new Experience
            {
                Id = Guid.NewGuid(),
                Company = "IPECOL",
                Position = "Software Developer",
                Description = "Developed an institutional management system using WPF, DevExpress, and SQL Server under the MVVM pattern. Designed user interfaces for handling and visualizing large volumes of data. Collaborated with multidisciplinary teams to ensure high-quality deliverables on schedule.",
                Period = new DateRange(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 30, 0, 0, 0, DateTimeKind.Utc)),
                Technologies = new List<string> { "WPF", "DevExpress", "SQL Server", "MVVM" },
                SortOrder = 3,
                CreatedAt = DateTime.UtcNow
            }
        };
        context.Experiences.AddRange(experiences);

        var education = new[]
        {
            new Education
            {
                Id = Guid.NewGuid(),
                Institution = "Facultad de Telem\u00e1tica \u2013 Universidad de Colima",
                Degree = "Bachelor's Degree",
                FieldOfStudy = "Software Engineering",
                Period = new DateRange(new DateTime(2020, 2, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 31, 0, 0, 0, DateTimeKind.Utc)),
                Gpa = null,
                Description = "Focused on object-oriented programming, data structures, web development, and software architecture principles.",
                SortOrder = 1,
                CreatedAt = DateTime.UtcNow
            }
        };
        context.Education.AddRange(education);

        var certifications = new[]
        {
            new Certification
            {
                Id = Guid.NewGuid(),
                Name = "Foundational C# Certification",
                Issuer = "Microsoft Learn",
                IssueDate = DateTime.UtcNow.AddMonths(-8),
                ExpiryDate = null,
                Url = "https://freecodecamp.org/certification/LuisAntonio/foundational-c-sharp-with-microsoft",
                CredentialId = null,
                SortOrder = 1,
                CreatedAt = DateTime.UtcNow
            }
        };
        context.Certifications.AddRange(certifications);

        var blogPosts = new[]
        {
            new BlogPost
            {
                Id = Guid.NewGuid(),
                Title = "Building Scalable Systems with Clean Architecture in ASP.NET MVC",
                Slug = "building-scalable-systems-clean-architecture-aspnet-mvc",
                Summary = "A practical guide to structuring enterprise ASP.NET MVC applications using Clean Architecture principles, Dependency Injection, and separation of concerns for maintainable and testable codebases.",
                Content = "Full article content would go here. This is placeholder text for the seed data.",
                PublishedAt = DateTime.UtcNow.AddDays(-7),
                Tags = new List<string> { "ASP.NET MVC", "Clean Architecture", "Dependency Injection", "C#" },
                ReadTimeMinutes = 8,
                IsPublished = true,
                CreatedAt = DateTime.UtcNow
            },
            new BlogPost
            {
                Id = Guid.NewGuid(),
                Title = "WPF Development with MVVM: Lessons from Building Enterprise Desktop Apps",
                Slug = "wpf-mvvm-enterprise-desktop-lessons",
                Summary = "Key patterns, pitfalls, and best practices learned from developing institutional management systems with WPF, DevExpress, and the MVVM pattern. Includes data binding strategies and UI performance tips.",
                Content = "Full article content would go here. This is placeholder text for the seed data.",
                PublishedAt = DateTime.UtcNow.AddDays(-14),
                Tags = new List<string> { "WPF", "MVVM", "DevExpress", "Desktop" },
                ReadTimeMinutes = 10,
                IsPublished = true,
                CreatedAt = DateTime.UtcNow
            }
        };
        context.BlogPosts.AddRange(blogPosts);

        await context.SaveChangesAsync();
        logger.LogInformation("Database seeded successfully with {ProfileCount} profile, {ProjectCount} projects, {SkillCount} skills, {ExperienceCount} experiences, {EducationCount} education, {CertificationCount} certifications, {BlogPostCount} blog posts.",
            1, projects.Length, skills.Length, experiences.Length, education.Length, certifications.Length, blogPosts.Length);
    }
}
