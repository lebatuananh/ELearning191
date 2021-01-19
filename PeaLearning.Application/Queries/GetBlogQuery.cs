using System;
using MediatR;
using PeaLearning.Application.Models;

namespace PeaLearning.Application.Queries
{
    public class GetBlogQuery:IRequest<BlogDto>
    {
        public Guid Id { get; private set; }

        public GetBlogQuery(Guid id)
        {
            Id = id;
        }
    }
}