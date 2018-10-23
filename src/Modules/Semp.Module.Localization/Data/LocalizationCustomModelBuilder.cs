using Microsoft.EntityFrameworkCore;
using Semp.Infrastructure;
using Semp.Infrastructure.Data;
using Semp.Infrastructure.Localization;

namespace Semp.Module.Localization.Data
{
    public class LocalizationCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Culture>().HasData(
               new Culture(GlobalConfiguration.DefaultCulture) { Name = "English (US)" }
            );
            modelBuilder.Entity<Culture>().ToTable("Localization_Culture");
            modelBuilder.Entity<Resource>().ToTable("Localization_Resource");
        }
    }
}
