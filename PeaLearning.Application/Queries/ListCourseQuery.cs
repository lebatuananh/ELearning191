using System.Collections.Generic;
using MediatR;
using PeaLearning.Application.Models;

namespace PeaLearning.Application.Queries
{
    public class ListCourseQuery:IRequest<List<CourseDto>>
    {
        
    }
}