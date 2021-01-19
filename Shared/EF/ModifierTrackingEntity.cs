using System;

namespace Shared.EF
{
    public interface IModifierTrackingEntity
    {
        Guid CreatedById { get; }
        Guid LastUpdatedById { get; }
    }

    public abstract class ModifierTrackingEntity : DateTrackingEntity, IModifierTrackingEntity
    {
        public string CreatedBy { get; private set; }
        public Guid CreatedById { get; private set; }

        public string LastUpdatedBy { get; private set; }
        public Guid LastUpdatedById { get; private set; }

        public void MarkCreated(Guid authorId, string authorName)
        {
            CreatedBy = authorName;
            LastUpdatedBy = authorName;
            CreatedById = authorId;
            LastUpdatedById = authorId;
        }

        public void MarkModified(Guid authorId, string authorName)
        {
            LastUpdatedBy = authorName;
            LastUpdatedById = authorId;
        }
    }
}
