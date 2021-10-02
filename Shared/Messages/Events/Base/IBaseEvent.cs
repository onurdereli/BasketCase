using System;

namespace Shared.Messages.Events.Base
{
    public interface IBaseEvent
    {
        public Guid Id { get; }

        public DateTime CreationDate { get; }
    }
}
