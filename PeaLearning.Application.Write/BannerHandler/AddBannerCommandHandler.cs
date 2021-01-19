using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeaLearning.Application.Commands.Banner;
using PeaLearning.Domain.AggregateModels.BannerAggregate;
using Shared.EF;

namespace PeaLearning.Application.Write.BannerHandler
{
    public class AddBannerCommandHandler : IRequestHandler<AddBannerCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBannerRepository _bannerRepository;

        public AddBannerCommandHandler(IUnitOfWork uow, IBannerRepository bannerRepository)
        {
            _uow = uow;
            _bannerRepository = bannerRepository;
        }

        public async Task<Unit> Handle(AddBannerCommand request, CancellationToken cancellationToken)
        {
            var banner = new Banner(request.Name, request.Thumbnail, request.Link, request.BannerPosition,request.BannerInPage);
            _bannerRepository.Add(banner);
            await _uow.CommitAsync();
            return Unit.Value;
        }
    }
}