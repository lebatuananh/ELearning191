using PeaLearning.Domain.AggregateModels.BlogAggregate;
using Shared.Infrastructure;

namespace PeaLearning.Infrastructure.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(PeaDbContext dbContext) : base(dbContext)
        {
        }
    }
}