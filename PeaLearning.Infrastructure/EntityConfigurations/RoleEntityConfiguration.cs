using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.UserAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");
        }
    }
}
