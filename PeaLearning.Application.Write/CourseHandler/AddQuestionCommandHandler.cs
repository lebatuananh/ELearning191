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
    public class AddQuestionCommandHandler : IRequestHandler<AddQuestionCommand, QuestionDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICourseRepository _courseRepository;

        public AddQuestionCommandHandler(IUnitOfWork uow, ICourseRepository courseRepository)
        {
            _uow = uow;
            _courseRepository = courseRepository;
        }

        public async Task<QuestionDto> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new ArgumentNullException("Course not found");
            }
            var question = new Question(request.Content, request.ParentId, request.Example, request.PictureUrl, request.AudioUrl, request.QuestionContent, request.Score);
            course.AddQuestion(request.LessonId, question);
            _courseRepository.Update(course);
            await _uow.CommitAsync();
            return new QuestionDto()
            {
                Id = question.Id,
                Content = question.Content,
                Example = question.Example,
                PictureUrl = question.PictureUrl,
                AudioUrl = question.AudioUrl,
                LessonId = question.LessonId,
                QuestionContentRaw = JsonConvert.SerializeObject(question.QuestionContent),
                ParentId = question.ParentId,
                Score = question.Score
            };
        }
    }
}