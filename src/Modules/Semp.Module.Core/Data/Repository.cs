using Semp.Infrastructure.Data;
using Semp.Infrastructure.Models;

namespace Semp.Module.Core.Data
{
    public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
       where T : class, IEntity<long>
    {
        public Repository(SimplDbContext context) : base(context)
        {
        }
    }
}
