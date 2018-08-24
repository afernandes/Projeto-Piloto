using System.Collections.Generic;
using Semp.Infrastructure.Models;

namespace Semp.Module.Localization.Models
{
    public class Culture : Entity<string>
    {
        public Culture(string id)
        {
            Id = id;
        }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public IList<Resource> Resources { get; set; }
    }
}
