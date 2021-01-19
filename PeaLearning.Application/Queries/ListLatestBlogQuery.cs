using MediatR;
using PeaLearning.Application.Models;
using System.Collections.Generic;

namespace PeaLearning.Application.Queries
{
    public class ListLatestBlogQuery : IRequest<IList<BlogDto>>
    {
        public int Limit { get; private set; }
        public ListLatestBlogQuery(int limit)
        {
            Limit = limit;
        }
    }
}
