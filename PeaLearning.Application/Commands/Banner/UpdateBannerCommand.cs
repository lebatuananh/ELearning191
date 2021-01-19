using System;
using MediatR;

namespace PeaLearning.Application.Commands.Banner
{
    public class UpdateBannerCommand : IRequest
    {
        public Guid Id { get; private set; }
        public string Name { private set; get; }
        public string Thumbnail { private set; get; }
        public string Link { private set; get; }
        public int BannerPosition { get; private set; }
        public int BannerInPage { get; private set; }


        public UpdateBannerCommand(Guid id, string name, string thumbnail, string link, int bannerPosition,
            int bannerInPage)
        {
            Id = id;
            Name = name;
            Thumbnail = thumbnail;
            Link = link;
            BannerPosition = bannerPosition;
            BannerInPage = bannerInPage;
        }
    }
}