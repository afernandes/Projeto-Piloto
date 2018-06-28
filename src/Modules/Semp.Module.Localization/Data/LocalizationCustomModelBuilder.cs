using Microsoft.EntityFrameworkCore;
using Semp.Infrastructure.Data;
using Semp.Module.Localization.Models;

namespace Semp.Module.Localization.Data
{
    public class LocalizationCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Culture>().HasData(
               new Culture("en-US") { Name = "English (US)", IsDefault = true }
            );
        }
    }
}
