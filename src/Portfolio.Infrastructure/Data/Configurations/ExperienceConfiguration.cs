using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;
using Portfolio.Domain.ValueObjects;
using Portfolio.Infrastructure.Data.Converters;

namespace Portfolio.Infrastructure.Data.Configurations;

public sealed class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable("Experiences");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Company).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Position).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(3000);
        builder.Property(x => x.CompanyUrl).HasMaxLength(500);

        builder.OwnsOne(x => x.Period, period =>
        {
            period.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
            period.Property(p => p.EndDate).HasColumnName("EndDate");
            period.Ignore(p => p.IsCurrent);
        });

        builder.Property(x => x.Technologies)
            .HasConversion<StringListValueConverter>()
            .HasMaxLength(4000);

        builder.Property(x => x.SortOrder).HasDefaultValue(0);
        builder.HasIndex(x => x.SortOrder);
    }
}
