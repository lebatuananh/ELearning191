using System;
using PeaLearning.Common.Models;

namespace PeaLearning.Api.Requests.Course
{
    public class AddQuestionRequest
    {
        public string Content { get; set; }
        public Guid? ParentId { get; set; }
        public string Example { get; set; }
        public string PictureUrl { get; set; }
        public string AudioUrl { get; set; }
        public int Score { get; set; }
        public QuestionContent QuestionContent { get; set; }
    }
}