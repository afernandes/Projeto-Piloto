using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Semp.Infrastructure.Models;

namespace Semp.Infrastructure.Localization
{
    public class Culture : Entity<string>
    {
        public Culture(string id)
        {
            Id = id;
        }

        [Required]
        [StringLength(450)]
        public string Name { get; set; }

        public IList<Resource> Resources { get; set; }
    }
}
