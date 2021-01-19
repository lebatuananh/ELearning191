using System;
using Shared.Dto;

namespace PeaLearning.Application.Models
{
    public class CourseRegistrationDto:ModifierTrackingDto
    {
        public Guid CourseId { get; set; }
        public Guid LearnerId { get; set; }
    }
}