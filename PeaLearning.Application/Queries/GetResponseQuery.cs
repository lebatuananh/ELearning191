using MediatR;
using PeaLearning.Application.Models;
using System;

namespace PeaLearning.Application.Queries
{
    public class GetResponseQuery : IRequest<AnalyzeResponseDto>
    {
        public Guid Id { get; private set; }
        public GetResponseQuery(Guid id)
        {
            Id = id;
        }
    }
}
