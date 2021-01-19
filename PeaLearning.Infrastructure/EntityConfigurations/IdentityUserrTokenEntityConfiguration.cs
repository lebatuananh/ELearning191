using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    class IdentityUserTokenEntityConfiguration : IEntityTypeConfiguration<IdentityUserToken<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
        {
            builder.ToTable("user_tokens");
            builder.HasKey(x => x.UserId); ;
        }
    }
}
