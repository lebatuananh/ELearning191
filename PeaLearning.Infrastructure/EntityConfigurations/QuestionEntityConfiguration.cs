using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.CourseAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    class QuestionEntityConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasOne(t => t.Lesson)
                .WithMany(c => c.Questions)
                .HasForeignKey(t => t.LessonId);
        }
    }
}
