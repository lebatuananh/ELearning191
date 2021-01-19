using PeaLearning.Domain.AggregateModels.TagAggregate;
using Shared.Infrastructure;

namespace PeaLearning.Infrastructure.Repositories
{
    public class TagRepository:Repository<Tag>,ITagRepository
    {
        public TagRepository(PeaDbContext dbContext) : base(dbContext)
        {
        }
    }
}