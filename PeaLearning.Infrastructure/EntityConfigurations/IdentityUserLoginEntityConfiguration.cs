using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    class IdentityUserLoginEntityConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
        {
            builder.ToTable("user_logins");
            builder.HasKey(x => x.UserId);
        }
    }
}
