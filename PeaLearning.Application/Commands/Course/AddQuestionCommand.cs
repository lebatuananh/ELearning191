using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Common.Models;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using System;
using System.Collections.Generic;

namespace PeaLearning.Application.Commands.Course
{
    public class AddQuestionCommand : IRequest<QuestionDto>
    {
        public Guid CourseId { get; private set; }
        public Guid LessonId { get; private set; }
        public string Content { get; private set; }
        public Guid? ParentId { get; private set; }
        public string Example { get; private set; }
        public string PictureUrl { get; private set; }
        public string AudioUrl { get; private set; }
        public int Score { get; private set; }
        public QuestionContent QuestionContent { get; private set; }

        public AddQuestionCommand(Guid courseId, Guid lessonId, string content, Guid? parentId, string example, string pictureUrl, string audioUrl, QuestionContent questionContent, int score)
        {
            CourseId = courseId;
            LessonId = lessonId;
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