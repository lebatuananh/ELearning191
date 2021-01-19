using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.TagAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    public class TagEntityConfiguration:IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("tags");
            builder.Property(c => c.Id).HasMaxLength(50)
                .IsRequired().IsUnicode(false).HasMaxLength(50);
        }
    }
}