using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.CourseAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    public class ResponseEntityConfiguration : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.HasOne(t => t.User)
               .WithMany(c => c.Responses)
               .HasForeignKey(t => t.LearnerId);
        }
    }
}
