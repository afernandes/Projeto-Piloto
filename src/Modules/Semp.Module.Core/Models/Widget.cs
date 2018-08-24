using System;
using Semp.Infrastructure.Models;

namespace Semp.Module.Core.Models
{
    public class Widget : Entity<string>
    {
        public Widget(string id)
        {
            Id = id;
            CreatedOn = DateTimeOffset.Now;
        }

        public string Code
        {
            get
            {
                return Id;
            }
        }

        public string Name { get; set; }

        public string ViewComponentName { get; set; }

        public string CreateUrl { get; set; }

        public string EditUrl { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public bool IsPublished { get; set; }
    }
}
