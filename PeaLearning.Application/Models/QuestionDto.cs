using Newtonsoft.Json;
using PeaLearning.Common.Models;
using Shared.Dto;
using Shared.Extensions;
using System;

namespace PeaLearning.Application.Models
{
    public class QuestionDto : DateTrackingDto
    {
        public string Content { get; set; }
        public Guid? ParentId { get; set; }
        public int OrderNumber { get; set; }
        public string Example { get; set; }
        public string PictureUrl { get; set; }
        public string AudioUrl { get; set; }
        public Guid LessonId { get; set; }
        public int Score { get; set; }
        public string QuestionContentRaw { get; set; }

        public QuestionContent QuestionContent
        {
            get => QuestionContentRaw.TryDeserialize<QuestionContent>();
            private set => QuestionContentRaw = JsonConvert.SerializeObject(value);
        }

        public string ParentContent { get; set; }
        public string ParentExample { get; set; }
        public QuestionResponse QuestionResponse { get; set; }
        public bool IsCorrect { get; set; }
    }
}