using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Semp.Infrastructure;
using Semp.Infrastructure.Data;
using Semp.Infrastructure.Models;
using Semp.Module.Core.Models;

namespace Semp.Module.Core.Data
{
    public class SempDbContext : SimplDbContext
    {
        public IConfiguration _configuration { get; set; }

        public SempDbContext(DbContextOptions<SempDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=STS07;Database=dtb_Integrator;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

    }
}
