using System.Collections.Generic;
using Semp.Infrastructure.Models;

namespace Semp.Module.Localization.Models
{
    public class Culture : EntityBase
    {
        public string Name { get; set; }

        public IList<Resource> Resources { get; set; }
    }
}
