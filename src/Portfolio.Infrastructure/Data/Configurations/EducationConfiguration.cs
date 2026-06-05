using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Data.Configurations;

public sealed class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.ToTable("Education");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Institution).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Degree).IsRequired().HasMaxLength(200);
        builder.Property(x => x.FieldOfStudy).HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(2000);

        builder.OwnsOne(x => x.Period, period =>
        {
            period.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
            period.Property(p => p.EndDate).HasColumnName("EndDate");
            period.Ignore(p => p.IsCurrent);
        });

        builder.Property(x => x.Gpa).HasColumnType("decimal(4,2)");
        builder.Property(x => x.SortOrder).HasDefaultValue(0);
        builder.HasIndex(x => x.SortOrder);
    }
}
