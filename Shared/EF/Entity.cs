using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.EF
{
    public abstract class Entity : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [NotMapped]
        public List<INotification> DomainEvents { get; private set; }

        public void AddDomainEvent(INotification eventItem)
        {
            DomainEvents = DomainEvents ?? new List<INotification>();
            DomainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            DomainEvents?.Remove(eventItem);
        }
    }
}
