using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.CourseAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasAlternateKey(t => t.Code);
            builder.Property(t => t.Code)
                .ValueGeneratedOnAdd();
            
        }
    }
}
