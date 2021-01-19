using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.BlogAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    public class BlogTagEntityConfiguration : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.ToTable("blog_tags");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.TagId).HasMaxLength(50).IsRequired()
                .IsUnicode(false).HasMaxLength(50);
        }
    }
}