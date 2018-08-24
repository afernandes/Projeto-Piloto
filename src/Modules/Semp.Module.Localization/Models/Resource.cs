using Semp.Infrastructure.Models;

namespace Semp.Module.Localization.Models
{
    public class Resource : Entity
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string CultureId { get; set; }

        public Culture Culture { get; set; }
    }
}
