using Microsoft.EntityFrameworkCore;

namespace Semp.Infrastructure.Data
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
