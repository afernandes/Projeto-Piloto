﻿using Semp.Infrastructure.Models;

namespace Semp.Module.Core.Models
{
    public class AppSetting : EntityBaseWithTypedId<string>
    {
        public AppSetting(string id)
        {
            Id = id;
        }

        public string Value { get; set; }

        public string Module { get; set; }

        public bool IsVisibleInCommonSettingPage { get; set; }
    }
}
