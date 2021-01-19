using System;
using System.ComponentModel.DataAnnotations.Schema;
using PeaLearning.Domain.AggregateModels.TagAggregate;
using Shared.EF;

namespace PeaLearning.Domain.AggregateModels.BlogAggregate
{
    public class BlogTag 
    {
        public Guid Id { get; set; }
        public Guid BlogId { set; get; }
        public string TagId { set; get; }

        [ForeignKey("BlogId")] public virtual Blog Blog { set; get; }

        [ForeignKey("TagId")] public virtual Tag Tag { set; get; }
    }
}