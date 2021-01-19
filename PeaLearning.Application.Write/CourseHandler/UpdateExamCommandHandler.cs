using MediatR;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.EF;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PeaLearning.Application.Write.CourseHandler
{
    public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICourseRepository _courseRepository;

        public UpdateExamCommandHandler(IUnitOfWork uow, ICourseRepository courseRepository)
        {
            _uow = uow;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new ArgumentNullException("Course not found");
            }
            course.UpdateLesson(request.LessonId, request.Title, request.Description, request.LessonType, request.Duration, request.IsActive);
            _courseRepository.Update(course);
            await _uow.CommitAsync();
            return Unit.Value;
        }
    }
}
