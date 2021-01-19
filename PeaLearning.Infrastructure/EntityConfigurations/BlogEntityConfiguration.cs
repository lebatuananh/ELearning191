using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.BlogAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    public class BlogEntityConfiguration:IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("blogs");
            
        }
    }
}