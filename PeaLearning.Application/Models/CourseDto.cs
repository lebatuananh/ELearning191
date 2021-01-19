using Shared.Dto;

namespace PeaLearning.Application.Models
{
    public class CourseDto : ModifierTrackingDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FriendlyUri { get; private set; }
        public int Code { get; private set; }
        public string Thumbnail { get; set; }
        public bool IsPrice { get; set; }
        public long? Price { get; set; }
        public string Link => string.Format("khoa-hoc/{0}-{1}", FriendlyUri, Code);
        public string LinkBuy => string.Format("mua-khoa-hoc/{0}/{1}", FriendlyUri, Id);
    }
}