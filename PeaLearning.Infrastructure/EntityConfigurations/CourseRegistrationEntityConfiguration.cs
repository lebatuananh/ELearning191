using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.CourseAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    public class CourseRegistrationEntityConfiguration : IEntityTypeConfiguration<CourseRegistration>
    {
        public void Configure(EntityTypeBuilder<CourseRegistration> builder)
        {
            builder.HasOne(t => t.Course)
                .WithMany(c => c.Registrations)
                .HasForeignKey(t => t.CourseId);

            builder.HasOne(t => t.Learner)
                .WithMany(l => l.Registrations)
                .HasForeignKey(t => t.LearnerId);

            builder.HasAlternateKey(r => new { r.LearnerId, r.CourseId });
        }
    }
}
