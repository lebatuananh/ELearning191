using MediatR;
using PeaLearning.Application.Models;
using System.Collections.Generic;

namespace PeaLearning.Application.Queries
{
    public class ListLatestCourseQuery : IRequest<IList<CourseDto>>
    {
        public int Limit { get; private set; }
        public ListLatestCourseQuery(int limit)
        {
            Limit = limit;
        }
    }
}
