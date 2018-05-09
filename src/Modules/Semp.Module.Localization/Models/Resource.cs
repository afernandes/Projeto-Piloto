﻿using Semp.Infrastructure.Models;

namespace Semp.Module.Localization.Models
{
    public class Resource : EntityBase
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public long CultureId { get; set; }

        public Culture Culture { get; set; }
    }
}
