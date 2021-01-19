namespace PeaLearning.Api.Requests.Banner
{
    public class AddBannerRequest
    {
        public string Name { set; get; }
        public string Thumbnail { set; get; }
        public string Link { set; get; }
        public int BannerPosition { get; set; }
        public int BannerInPage { get;  set; }

    }
}