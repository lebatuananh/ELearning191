using MediatR;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.EF;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PeaLearning.Application.Write.CourseHandler
{
    public class SaveResponseQueryHandler : IRequestHandler<SaveResponseQuery, Guid>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _uow;

        public SaveResponseQueryHandler(ICourseRepository courseRepository, IUnitOfWork uow)
        {
            _courseRepository = courseRepository;
            _uow = uow;
        }

        public async Task<Guid> Handle(SaveResponseQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new ArgumentNullException("Course not found");
            }
            course.SaveResponse(request.LessonId, request.SubmittedDate, request.QuestionResponses, request.LearnerId, request.CompletedDuration);
            _courseRepository.Update(course);
            await _uow.CommitAsync();
            return course.LatestResponse(request.LessonId);
        }
    }
}
