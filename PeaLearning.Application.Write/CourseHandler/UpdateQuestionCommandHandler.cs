using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Application.Models;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.EF;

namespace PeaLearning.Application.Write.CourseHandler
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, QuestionDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICourseRepository _courseRepository;

        public UpdateQuestionCommandHandler(IUnitOfWork uow, ICourseRepository courseRepository)
        {
            _uow = uow;
            _courseRepository = courseRepository;
        }

        public async Task<QuestionDto> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new ArgumentNullException("Course not found");
            }
            course.UpdateQuestion(request.LessonId, request.QuestionId, request.Content, request.ParentId, request.Example, request.PictureUrl, request.AudioUrl, request.QuestionContent, request.Score);
            _courseRepository.Update(course);
            await _uow.CommitAsync();
            return new QuestionDto()
            {
                Id = request.QuestionId,
                Content = request.Content,
                Example = request.Example,
                PictureUrl = request.PictureUrl,
                AudioUrl = request.AudioUrl,
                LessonId = request.LessonId,
                QuestionContentRaw = JsonConvert.SerializeObject(request.QuestionContent),
                ParentId = request.ParentId,
                Score = request.Score
            };
        }
    }
}