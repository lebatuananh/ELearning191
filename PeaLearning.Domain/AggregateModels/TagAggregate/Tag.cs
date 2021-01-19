using System.ComponentModel.DataAnnotations;
using Shared.EF;

namespace PeaLearning.Domain.AggregateModels.TagAggregate
{
    public class Tag : IAggregateRoot
    {
        public Tag(string id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public string Id { get; private set; }
        [MaxLength(50)] [Required] public string Name { get; private set; }
        [MaxLength(50)] [Required] public string Type { get; private set; }
    }
}