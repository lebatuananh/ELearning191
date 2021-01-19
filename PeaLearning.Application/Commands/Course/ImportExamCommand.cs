using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace PeaLearning.Application.Commands.Course
{
    public class ImportExamCommand : IRequest
    {
        public Guid CourseId { get; private set; }
        public  IFormFile File { get; private set; }

        public ImportExamCommand(Guid courseId, IFormFile file)
        {
            CourseId = courseId;
            File = file;
        }
    }
}
