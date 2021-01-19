using Newtonsoft.Json;
using PeaLearning.Common.Models;
using Shared.EF;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PeaLearning.Domain.AggregateModels.CourseAggregate
{
    public class Question : DateTrackingEntity
    {
        public string Content { get; private set; }
        public Guid? ParentId { get; private set; }
        public int OrderNumber { get; private set; }
        public string Example { get; private set; }
        public string PictureUrl { get; private set; }
        public string AudioUrl { get; private set; }
        public Guid LessonId { get; private set; }
        public int Score { get; set; }
        public virtual Lesson Lesson { get; private set; }
        public string QuestionContentRaw { get; private set; }

        [NotMapped]
        public QuestionContent QuestionContent
        {
            get => QuestionContentRaw.TryDeserialize<QuestionContent>();
            private set => QuestionContentRaw = JsonConvert.SerializeObject(value);
        }

        public Question()
        {
        }

        public Question(string content, Guid? parentId, string example, string pictureUrl, string audioUrl, QuestionContent questionContent, int score) : this()
        {
            Content = content;
            ParentId = parentId;
            Example = example;
            PictureUrl = pictureUrl;
            AudioUrl = audioUrl;
            QuestionContent = questionContent;
            Score = score;
        }

        public void Update(string content, Guid? parentId, string example, string pictureUrl, string audioUrl, QuestionContent questionContent, int score)
        {
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