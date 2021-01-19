using Shared.EF;

namespace PeaLearning.Domain.AggregateModels.BlogAggregate
{
    public interface IBlogRepository : IRepository<Blog>
    {
    }
}