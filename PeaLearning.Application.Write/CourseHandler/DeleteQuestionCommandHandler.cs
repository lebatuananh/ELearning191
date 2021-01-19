using MediatR;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.EF;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PeaLearning.Application.Write.CourseHandler
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICourseRepository _courseRepository;

        public DeleteQuestionCommandHandler(IUnitOfWork uow, ICourseRepository courseRepository)
        {
            _uow = uow;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new ArgumentNullException("Course not found");
            }
            course.RemoveQuestion(request.LessonId, request.QuestionId);
            _courseRepository.Update(course);
            await _uow.CommitAsync();
            return Unit.Value;
        }
    }
}
