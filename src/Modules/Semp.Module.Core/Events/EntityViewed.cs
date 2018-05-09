using MediatR;

namespace Semp.Module.Core.Events
{
    public class EntityViewed : INotification
    {
        public long EntityId { get; set; }

        public long EntityTypeId { get; set; }
    }
}
