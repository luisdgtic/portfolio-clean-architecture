using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data.Converters;

namespace Portfolio.Infrastructure.Data.Configurations;

public sealed class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("BlogPosts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(300);
        builder.Property(x => x.Slug).IsRequired().HasMaxLength(300);
        builder.Property(x => x.Summary).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Content).IsRequired().HasColumnType("text");
        builder.Property(x => x.PublishedAt).IsRequired();

        builder.Property(x => x.Tags)
            .HasConversion<StringListValueConverter>()
            .HasMaxLength(4000);

        builder.Property(x => x.ReadTimeMinutes).HasDefaultValue(1);
        builder.Property(x => x.IsPublished).HasDefaultValue(false);

        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.PublishedAt);
        builder.HasIndex(x => x.IsPublished);
    }
}
