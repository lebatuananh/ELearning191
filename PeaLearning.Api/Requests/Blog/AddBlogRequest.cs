namespace PeaLearning.Api.Requests.Blog
{
    public class AddBlogRequest
    {
        public string Name { set; get; }
        public string Thumbnail { set; get; }
        public string Description { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public string SeoPageTitle { set; get; }
        public string SeoAlias { set; get; }
        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }
        public bool Status { set; get; }
        public string Tags { get; set; }
        public string Content { get; set; }
    }
}