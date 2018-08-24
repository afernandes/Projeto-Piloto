using Semp.Infrastructure.Models;

namespace Semp.Module.Core.Models
{
    public class Media : Infrastructure.Models.Entity
    {
        public string Caption { get; set; }

        public int FileSize { get; set; }

        public string FileName { get; set; }

        public MediaType MediaType { get; set; }
    }
}
