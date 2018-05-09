using Microsoft.AspNetCore.Identity;
using Semp.Infrastructure.Models;
using System.Collections.Generic;

namespace Semp.Module.Core.Models
{
    public class Role : IdentityRole<long>, IEntityWithTypedId<long>
    {
        public IList<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
