﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Semp.Infrastructure.Data;
using Semp.Module.Localization.Models;

namespace Semp.Module.Localization
{
    public class EfStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IRepository<Resource> _resourceRepository;
        private IList<ResourceString> _resourceStrings;

        public EfStringLocalizerFactory(IRepository<Resource> resourceRepository)
        {
            _resourceRepository = resourceRepository;
            LoadResources();
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new EfStringLocalizer(_resourceStrings);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new EfStringLocalizer(_resourceStrings);
        }

        private void LoadResources()
        {
            _resourceStrings = _resourceRepository.Query().Select(x => new ResourceString
            {
                Culture = x.CultureId,
                Key = x.Key,
                Value = x.Value
            }).ToList();
        }
    }
}