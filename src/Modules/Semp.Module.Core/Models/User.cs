﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Semp.Infrastructure.Models;

namespace Semp.Module.Core.Models
{
    public class User : IdentityUser<long>, IEntityWithTypedId<long>
    {
        public User()
        {
            CreatedOn = DateTimeOffset.Now;
            UpdatedOn = DateTimeOffset.Now;
        }

        public Guid UserGuid { get; set; }

        public string FullName { get; set; }

        public long? VendorId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        //public IList<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

        //public UserAddress DefaultShippingAddress { get; set; }

        public long? DefaultShippingAddressId { get; set; }

        //public UserAddress DefaultBillingAddress { get; set; }

        public long? DefaultBillingAddressId { get; set; }

        public IList<UserRole> Roles { get; set; } = new List<UserRole>();

        //public IList<UserCustomerGroup> CustomerGroups { get; set; } = new List<UserCustomerGroup>();
    }
}
