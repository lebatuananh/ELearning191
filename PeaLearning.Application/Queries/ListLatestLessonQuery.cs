using MediatR;
using PeaLearning.Application.Models;
using System.Collections.Generic;

namespace PeaLearning.Application.Queries
{
    public class ListLatestLessonQuery : IRequest<IList<LessonInfoDto>>
    {
        public int Limit { get; private set; }
        public ListLatestLessonQuery(int limit)
        {
            Limit = limit;
        }
    }
}
