using System;
using MediatR;

namespace PeaLearning.Application.Commands.Blog
{
    public class DeleteBlogCommand:IRequest
    {
        public Guid Id { get; private set; }

        public DeleteBlogCommand(Guid id)
        {
            Id = id;
        }
    }
}