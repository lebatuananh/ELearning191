using System;
using MediatR;
using PeaLearning.Application.Models;

namespace PeaLearning.Application.Queries
{
    public class GetBannerQuery : IRequest<BannerDto>
    {
        public GetBannerQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}