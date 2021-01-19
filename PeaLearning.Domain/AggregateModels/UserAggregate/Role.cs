using Microsoft.AspNetCore.Identity;
using System;

namespace PeaLearning.Domain.AggregateModels.UserAggregate
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
        }

        public Role(string name, string description) : base(name)
        {
            Description = description;
        }
        public string Description { get; private set; }
    }
}
