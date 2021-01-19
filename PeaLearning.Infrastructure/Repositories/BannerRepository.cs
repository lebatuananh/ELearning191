using PeaLearning.Domain.AggregateModels.BannerAggregate;
using Shared.Infrastructure;

namespace PeaLearning.Infrastructure.Repositories
{
    public class BannerRepository:Repository<Banner>,IBannerRepository
    {
        public BannerRepository(PeaDbContext dbContext) : base(dbContext)
        {
        }
    }
}