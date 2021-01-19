using MediatR;
using PeaLearning.Application.Models;
using System.Collections.Generic;

namespace PeaLearning.Application.Queries
{
    public class ListLessonByCourseQuery : IRequest<IList<LessonInfoDto>>
    {
        public int Code { get; private set; }

        public ListLessonByCourseQuery(int code)
        {
            Code = code;
        }
    }
}
