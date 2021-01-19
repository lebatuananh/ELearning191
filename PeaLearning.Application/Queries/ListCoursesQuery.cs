using MediatR;
using PeaLearning.Application.Models;
using Shared.Dto;

namespace PeaLearning.Application.Queries
{
    public class ListCoursesQuery : ListQuery, IRequest<QueryResult<CourseDto>>
    {
        public ListCoursesQuery(
           int skip,
           int take,
           string query = null
           )
           : base(skip, take, query)
        {
        }
    }
}
