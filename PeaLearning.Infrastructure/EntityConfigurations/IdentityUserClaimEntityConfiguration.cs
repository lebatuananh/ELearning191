using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    class IdentityUserClaimEntityConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
        {
            builder.ToTable("user_claims");
            builder.HasKey(x => x.Id);
        }
    }
}
