using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.BannerAggregate;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    public class BannerEntityConfiguration:IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.ToTable("banners");
        }
    }
}