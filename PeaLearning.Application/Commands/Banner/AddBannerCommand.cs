using MediatR;

namespace PeaLearning.Application.Commands.Banner
{
    public class AddBannerCommand : IRequest
    {
        public AddBannerCommand(string name, string thumbnail, string link, int bannerPosition,int bannerInPage)
        {
            Name = name;
            Thumbnail = thumbnail;
            Link = link;
            BannerPosition = bannerPosition;
            BannerInPage = bannerInPage;
        }

        public string Name { private set; get; }
        public string Thumbnail { private set; get; }
        public string Link { private set; get; }
        public int BannerPosition { get; private set; }
        public int BannerInPage { get; private set; }
    }
}