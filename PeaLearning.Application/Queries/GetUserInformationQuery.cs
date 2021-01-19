using System;
using MediatR;
using PeaLearning.Application.Models;

namespace PeaLearning.Application.Queries
{
    public class GetUserInformationQuery:IRequest<UserDto>
    {
        public Guid Id { get; private set; }

        public GetUserInformationQuery(Guid id)
        {
            Id = id;
        }
    }
}