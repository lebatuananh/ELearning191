using System;

namespace Shared.Dto
{
    public class ModifierTrackingDto : ModifierTrackingDto<Guid>
    {
    }

    public class ModifierTrackingDto<T> : DateTrackingDto<T>
    {
        public Guid CreatedById { get; set; }
        public Guid LastUpdatedById { get; set; }
    }
}
