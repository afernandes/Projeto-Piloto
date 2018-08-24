using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Semp.Infrastructure.Models;

namespace Semp.Module.Core.Models
{
    public class User : IdentityUser<long>, IEntity<long>
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

        public string RefreshTokenHash { get; set; }

        public IList<UserRole> Roles { get; set; } = new List<UserRole>();

        public bool IsTransient()
        {
            if (EqualityComparer<long>.Default.Equals(Id, default(long)))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(long) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(long) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }

        //public IList<CustomerGroupUser> CustomerGroups { get; set; } = new List<CustomerGroupUser>();
    }
}
