using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.EF;

namespace PeaLearning.Application.Write.CourseHandler
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseCommandHandler(IUnitOfWork uow, ICourseRepository courseRepository)
        {
            _uow = uow;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);
            if (course == null)
                throw new ArgumentNullException("Course not found");
            course.Update(request.Title, request.Description, request.Thumbnail, request.IsPrice, request.Price);
            _courseRepository.Update(course);
            await _uow.CommitAsync();
            return Unit.Value;
        }
    }
}