using Semp.Infrastructure.Models;

namespace Semp.Module.Core.Models
{
    public class WidgetZone : Infrastructure.Models.Entity
    {
        public WidgetZone(long id)
        {
            Id = id;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
