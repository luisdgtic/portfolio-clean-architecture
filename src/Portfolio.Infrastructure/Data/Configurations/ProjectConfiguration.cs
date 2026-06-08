using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data.Converters;

namespace Portfolio.Infrastructure.Data.Configurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(3000);

        builder.Property(x => x.ImageUrls)
            .HasConversion<StringListValueConverter>()
            .HasMaxLength(4000);

        builder.Property(x => x.GitHubUrl).HasMaxLength(500);
        builder.Property(x => x.LiveUrl).HasMaxLength(500);

        builder.Property(x => x.TechStack)
            .HasConversion<StringListValueConverter>()
            .HasMaxLength(4000);

        builder.Property(x => x.IsFeatured).HasDefaultValue(false);
        builder.Property(x => x.SortOrder).HasDefaultValue(0);

        builder.HasIndex(x => x.IsFeatured);
        builder.HasIndex(x => x.SortOrder);
    }
}
