using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.EF;

namespace PeaLearning.Application.Write.CourseHandler
{
    public class AddCourseCommandHandler : IRequestHandler<AddCourseCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICourseRepository _courseRepository;

        public AddCourseCommandHandler(IUnitOfWork uow, ICourseRepository courseRepository)
        {
            _uow = uow;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course(request.Title, request.Description, request.Thumbnail, request.IsPrice,
                request.Price);
            _courseRepository.Add(course);
            await _uow.CommitAsync();
            return Unit.Value;
        }
    }
}