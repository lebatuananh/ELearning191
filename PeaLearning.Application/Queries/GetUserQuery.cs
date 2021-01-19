using System;
using MediatR;
using PeaLearning.Application.Models;

namespace PeaLearning.Application.Queries
{
    public class GetUserQuery:IRequest<UserDto>
    {
        public Guid Id { get; private set; }

        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}