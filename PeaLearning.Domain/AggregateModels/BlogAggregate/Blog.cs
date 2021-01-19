using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shared.EF;

namespace PeaLearning.Domain.AggregateModels.BlogAggregate
{
    public class Blog : ModifierTrackingEntity, IAggregateRoot
    {
        public Blog(string name, string thumbnail, string description, bool? homeFlag, bool? hotFlag,
            string seoPageTitle, string seoAlias, string seoKeywords, string seoDescription, bool status, string tags,
            string content)
        {
            Name = name;
            Thumbnail = thumbnail;
            Description = description;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
            Status = status;
            Tags = tags;
            Content = content;
        }

        public void Update(string name, string thumbnail, string description, bool? homeFlag, bool? hotFlag,
            string seoPageTitle, string seoAlias, string seoKeywords, string seoDescription, bool status, string tags,
            string content)
        {
            Name = name;
            Thumbnail = thumbnail;
            Description = description;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
            Status = status;
            Tags = tags;
            Content = content;
        }

        [Required] [MaxLength(256)] public string Name { private set; get; }
        [MaxLength(256)] public string Thumbnail { private set; get; }
        [MaxLength(500)] public string Description { private set; get; }
        public string Content { get; private set; }
        public bool? HomeFlag { private set; get; }
        public bool? HotFlag { private set; get; }
        public int? ViewCount { private set; get; }
        [MaxLength(256)] public string SeoPageTitle { private set; get; }
        [MaxLength(256)] public string SeoAlias { private set; get; }
        [MaxLength(256)] public string SeoKeywords { private set; get; }
        [MaxLength(256)] public string SeoDescription { private set; get; }
        public bool Status { private set; get; }
        public string Tags { get; private set; }
        public virtual ICollection<BlogTag> BlogTags { set; get; }
    }
}