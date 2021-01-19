using System;
using MediatR;

namespace PeaLearning.Application.Commands.Banner
{
    public class DeleteBannerCommand:IRequest
    {
        public Guid Id { get; private set; }

        public DeleteBannerCommand(Guid id)
        {
            Id = id;
        }
    }
}