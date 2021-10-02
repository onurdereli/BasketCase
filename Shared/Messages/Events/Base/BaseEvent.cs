using System;

namespace Shared.Messages.Events.Base
{
    public class BaseEvent : IBaseEvent
    {
        public BaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
        
        public BaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }
        
        public Guid Id { get; private set; }
        
        public DateTime CreationDate { get; private set; }
    }
}
