using System.ComponentModel.DataAnnotations;
using Shared.EF;

namespace PeaLearning.Domain.AggregateModels.BannerAggregate
{
    public class Banner : ModifierTrackingEntity, IAggregateRoot
    {
        [Required] [MaxLength(256)] public string Name { private set; get; }
        [MaxLength(256)] public string Thumbnail { private set; get; }
        [MaxLength(500)] public string Link { private set; get; }
        public int BannerPosition { get; private set; }
        public int BannerInPage { get; private set; }

        public Banner()
        {
        }

        public Banner(string name, string thumbnail, string link, int bannerPosition, int bannerInPage)
        {
            Name = name;
            Thumbnail = thumbnail;
            Link = link;
            BannerPosition = bannerPosition;
            BannerInPage = bannerInPage;
        }

        public void Update(string name, string thumbnail, string link, int bannerPosition, int bannerInPage)
        {
            Name = name;
            Thumbnail = thumbnail;
            Link = link;
            BannerPosition = bannerPosition;
            BannerInPage = bannerInPage;
        }
    }
}