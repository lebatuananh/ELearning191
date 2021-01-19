using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    class IdentityUserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.ToTable("user_roles");
            builder.HasKey(x => new { x.RoleId, x.UserId }); ;
        }
    }
}
