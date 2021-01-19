using System;

namespace Shared.Dto
{
    public abstract class DateTrackingDto : DateTrackingDto<Guid>
    {
    }

    public abstract class DateTrackingDto<T> : EntityDto<T>
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastUpdatedDate { get; set; }
    }
}
