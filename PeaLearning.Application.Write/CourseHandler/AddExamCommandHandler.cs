using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.EF;

namespace PeaLearning.Application.Write.CourseHandler
{
    public class AddExamCommandHandler : IRequestHandler<AddExamCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICourseRepository _courseRepository;

        public AddExamCommandHandler(IUnitOfWork uow, ICourseRepository courseRepository)
        {
            _uow = uow;
            _courseRepository = courseRepository;
        }

        public async Task<Guid> Handle(AddExamCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new ArgumentNullException("Course not found");
            }
            var lesson = new Lesson(request.Title, request.Description, request.LessonType, request.Duration, request.IsActive);
            course.AddLesson(lesson);
            _courseRepository.Update(course);
            await _uow.CommitAsync();
            return lesson.Id;
        }
    }
}
