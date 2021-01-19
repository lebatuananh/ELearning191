using PeaLearning.Common.Models;
using Shared.EF;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeaLearning.Domain.AggregateModels.CourseAggregate
{
    //Khóa học
    public class Course : ModifierTrackingEntity, IAggregateRoot
    {
        public string FriendlyUri { get; private set; }
        public int Code { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Thumbnail { get; private set; }
        public bool IsPrice { get; set; }
        public long? Price { get; set; }
        public virtual IList<CourseRegistration> Registrations { get; private set; }
        public virtual IList<Lesson> Lessons { get; private set; }

        public Course(string title, string description, string thumbnail, bool isPrice, long? price)
        {
            Title = title;
            FriendlyUri = Title.ToSlug();
            Description = description;
            Thumbnail = thumbnail;
            IsPrice = isPrice;
            Price = price;
        }

        public void Update(string title, string description, string thumbnail, bool isPrice, long? price)
        {
            Title = title;
            FriendlyUri = Title.ToSlug();
            Description = description;
            Thumbnail = thumbnail;
            IsPrice = isPrice;
            Price = price;
        }

        public void AddLesson(Lesson newLesson)
        {
            Lessons ??= new List<Lesson>();
            Lessons.Add(newLesson);
        }

        public void UpdateLesson(Guid lessonId, string title, string description, LessonType lessonType, int duration,
            bool isActive)
        {
            var lesson = Lessons.FirstOrDefault(l => l.Id == lessonId);
            if (lesson == null)
            {
                throw new ArgumentNullException("Exam not found");
            }

            lesson.Update(title, description, lessonType, duration, isActive);
        }

        public void AddQuestion(Guid lessonId, Question question)
        {
            Lessons ??= new List<Lesson>();
            var lesson = Lessons.FirstOrDefault(x => x.Id == lessonId);
            if (lesson != null)
            {
                lesson.AddQuestion(question);
            }
        }

        public void RemoveLesson(Guid lessonId)
        {
            var lesson = Lessons.FirstOrDefault(l => l.Id == lessonId);
            if (lesson == null)
            {
                throw new ArgumentNullException("Exam not found");
            }

            Lessons.Remove(lesson);
        }

        public void UpdateQuestion(Guid lessonId, Guid questionId, string content, Guid? parentId, string example,
            string pictureUrl, string audioUrl, QuestionContent questionContent, int score)
        {
            Lessons ??= new List<Lesson>();
            var lesson = Lessons.FirstOrDefault(x => x.Id == lessonId);
            if (lesson != null)
            {
                lesson.UpdateQuestion(questionId, content, parentId, example, pictureUrl, audioUrl, questionContent,
                    score);
            }
        }

        public void RemoveQuestion(Guid lessonId, Guid questionId)
        {
            var lesson = Lessons.FirstOrDefault(l => l.Id == lessonId);
            if (lesson == null)
            {
                throw new ArgumentNullException("Exam not found");
            }

            lesson.RemoveQuestion(questionId);
        }

        public void SaveResponse(Guid lessonId, DateTimeOffset submittedDate, IList<QuestionResponse> questionResponses,
            Guid learnerId, int completedDuration)
        {
            Lessons ??= new List<Lesson>();
            var lesson = Lessons.FirstOrDefault(x => x.Id == lessonId);
            if (lesson != null)
            {
                lesson.AddResponse(submittedDate, questionResponses, learnerId, completedDuration);
            }
        }

        public Guid LatestResponse(Guid lessonId)
        {
            Lessons ??= new List<Lesson>();
            var lesson = Lessons.FirstOrDefault(x => x.Id == lessonId);
            if (lesson != null)
            {
                return lesson.Responses.OrderByDescending(r => r.CreatedDate).FirstOrDefault() != null
                    ? lesson.Responses.OrderByDescending(r => r.CreatedDate).FirstOrDefault().Id
                    : Guid.Empty;
            }

            return Guid.Empty;
        }
    }
}