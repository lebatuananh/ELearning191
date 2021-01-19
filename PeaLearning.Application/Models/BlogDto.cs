using PeaLearning.Common;
using PeaLearning.Common.Utils;
using Shared.Dto;
using System;

namespace PeaLearning.Application.Models
{
    public class BlogDto : DateTrackingDto
    {
        public Guid Id { get; set; }
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
        public int ViewCount { get; set; }
        public string Link => string.Format("/tin-tuc/{0}-aid{1}", StringUtils.UnicodeToUnsignCharAndDash(Name), Id);
        public string AvatarUrl => string.Format("{0}{1}", StaticVariable.DomainImage, Thumbnail);
    }
}