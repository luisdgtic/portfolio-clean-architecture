using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Data.Configurations;

public sealed class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Summary).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.PhotoUrl).HasMaxLength(500);
        builder.Property(x => x.ResumeUrl).HasMaxLength(500);
        builder.Property(x => x.Email).HasMaxLength(200);
        builder.Property(x => x.Phone).HasMaxLength(50);
        builder.Property(x => x.Location).HasMaxLength(200);
        builder.Property(x => x.LinkedInUrl).HasMaxLength(500);
        builder.Property(x => x.GitHubUrl).HasMaxLength(500);
        builder.Property(x => x.TwitterUrl).HasMaxLength(500);
        builder.Property(x => x.WebsiteUrl).HasMaxLength(500);
    }
}
