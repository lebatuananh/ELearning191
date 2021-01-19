using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.EF;

namespace PeaLearning.Application.Write.CourseHandler
{
    public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICourseRepository _courseRepository;

        public DeleteExamCommandHandler(IUnitOfWork uow, ICourseRepository courseRepository)
        {
            _uow = uow;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new ArgumentNullException("Course not found");
            }
            course.RemoveLesson(request.LessonId);
            _courseRepository.Update(course);
            await _uow.CommitAsync();
            return Unit.Value;
        }
    }
}
