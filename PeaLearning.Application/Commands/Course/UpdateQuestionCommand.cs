using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Common.Models;
using System;

namespace PeaLearning.Application.Commands.Course
{
    public class UpdateQuestionCommand : IRequest<QuestionDto>
    {
        public Guid CourseId { get; private set; }
        public Guid LessonId { get; private set; }
        public Guid QuestionId { get; private set; }
        public string Content { get; private set; }
        public Guid? ParentId { get; private set; }
        public string Example { get; private set; }
        public string PictureUrl { get; private set; }
        public string AudioUrl { get; private set; }
        public int Score { get; private set; }
        public QuestionContent QuestionContent { get; private set; }

        public UpdateQuestionCommand(Guid courseId, Guid lessonId, Guid questionId, string content, Guid? parentId, string example, string pictureUrl, string audioUrl, QuestionContent questionContent, int score)
        {
            CourseId = courseId;
            LessonId = lessonId;
            QuestionId = questionId;
            Content = content;
            ParentId = parentId;
            Example = example;
            PictureUrl = pictureUrl;
            AudioUrl = audioUrl;
            QuestionContent = questionContent;
            Score = score;
        }
    }
}