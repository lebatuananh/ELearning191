using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.CourseAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    class LessonEntityConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasOne(t => t.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(t => t.CourseId);
        }
    }
}
