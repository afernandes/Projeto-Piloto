using Semp.Infrastructure.Models;

namespace Semp.Module.Core.Models
{
    public class Entity : Infrastructure.Models.Entity
    {
        public string Slug { get; set; }

        public string Name { get; set; }

        public long EntityId { get; set; }

        public string EntityTypeId { get; set; }

        public EntityType EntityType { get; set; }
    }
}
