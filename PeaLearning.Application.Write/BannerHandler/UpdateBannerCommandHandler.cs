using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeaLearning.Application.Commands.Banner;
using PeaLearning.Domain.AggregateModels.BannerAggregate;
using Shared.EF;

namespace PeaLearning.Application.Write.BannerHandler
{
    public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBannerRepository _bannerRepository;

        public UpdateBannerCommandHandler(IUnitOfWork unitOfWork, IBannerRepository bannerRepository)
        {
            _unitOfWork = unitOfWork;
            _bannerRepository = bannerRepository;
        }

        public async Task<Unit> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
        {
            var banner = await _bannerRepository.GetByIdAsync(request.Id);
            if (banner == null)
            {
                throw new ArgumentNullException("Banner not found");
            }

            banner.Update(request.Name, request.Thumbnail, request.Link, request.BannerPosition,request.BannerInPage);
            _bannerRepository.Update(banner);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }
}