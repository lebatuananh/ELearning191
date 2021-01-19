using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.Infrastructure;

namespace PeaLearning.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(PeaDbContext dbContext) : base(dbContext)
        {
        }
    }
}
