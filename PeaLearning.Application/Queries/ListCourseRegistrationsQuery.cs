using System;
using MediatR;
using PeaLearning.Application.Models;
using Shared.Dto;

namespace PeaLearning.Application.Queries
{
    public class ListCourseRegistrationsQuery : ListQuery, IRequest<QueryResult<UserRegistrationDto>>
    {
        public ListCourseRegistrationsQuery(int skip, int take, string query = null) : base(skip, take,
            query)
        {
        }
    }
}