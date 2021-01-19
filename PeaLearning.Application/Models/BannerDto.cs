using System;

namespace PeaLearning.Application.Models
{
    public class BannerDto
    {
        public Guid Id { get; set; }
        public string Name { set; get; }
        public string Thumbnail { set; get; }
        public string Link { set; get; }
        public int BannerPosition { get; set; }
        public int BannerInPage { get; set; }
    }
}