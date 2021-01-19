using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.UserAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
        }
    }
}
