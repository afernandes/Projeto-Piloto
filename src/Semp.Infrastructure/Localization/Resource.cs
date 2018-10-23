using System.ComponentModel.DataAnnotations;
using Semp.Infrastructure.Models;

namespace Semp.Infrastructure.Localization
{
    public class Resource : Entity
    {
        [Required]
        [StringLength(450)]
        public string Key { get; set; }

        public string Value { get; set; }

        [Required]
        public string CultureId { get; set; }

        public Culture Culture { get; set; }
    }
}
