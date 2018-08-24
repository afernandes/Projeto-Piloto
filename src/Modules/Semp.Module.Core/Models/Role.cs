using Microsoft.AspNetCore.Identity;
using Semp.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Semp.Module.Core.Models
{
    public class Role : IdentityRole<long>, IEntity<long>
    {
        public IList<UserRole> Users { get; set; } = new List<UserRole>();

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
    }
}
