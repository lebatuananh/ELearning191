using PeaLearning.Common.Models;
using Shared.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeaLearning.Domain.AggregateModels.CourseAggregate
{
    //Đề thi
    public class Lesson : DateTrackingEntity
    {
        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public LessonType LessonType { get; private set; }
        public int Duration { get; private set; } // in minutes
        public bool IsActive { get; private set; }
        public virtual Course Course { get; private set; }
        public virtual IList<Question> Questions { get; private set; }
        public virtual IList<Response> Responses { get; private set; }

        public Lesson(string title, string description, LessonType lessonType, int duration, bool isActive)
        {
            Title = title;
            Description = description;
            LessonType = lessonType;
            Duration = duration;
            IsActive = isActive;
        }

        public void Update(string title, string description, LessonType lessonType, int duration, bool isActive)
        {
            Title = title;
            Description = description;
            LessonType = lessonType;
            Duration = duration;
            IsActive = isActive;
        }

        public void AddQuestion(Question question)
        {
            Questions ??= new List<Question>();
            Questions.Add(question);
        }

        public void RemoveQuestion(Guid questionId)
        {
            var question = Questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                throw new ArgumentNullException("Question not found");
            }
            Questions.Remove(question);
            if (question.QuestionContent.QuestionType == QuestionType.Section)
            {
                foreach (var item in Questions.Where(q => q.ParentId == questionId).ToList())
                {
                    Questions.Remove(item);
                }
            }
        }

        public void UpdateQuestion(Guid questionId, string content, Guid? parentId, string example, string pictureUrl, string audioUrl, QuestionContent questionContent, int score)
        {
            var question = Questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                throw new ArgumentNullException("Question not found");
            }
            question.Update(content, parentId, example, pictureUrl, audioUrl, questionContent, score);
        }

        public void AddResponse(DateTimeOffset submittedDate, IList<QuestionResponse> questionResponses, Guid learnerId, int completedDuration)
        {
            Responses ??= new List<Response>();
            var response = new Response(submittedDate, questionResponses, learnerId, completedDuration);
            Responses.Add(response);
        }
    }
}