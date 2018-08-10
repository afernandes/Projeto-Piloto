using System.Linq;
using Microsoft.EntityFrameworkCore;
using Semp.Infrastructure.Data;

namespace Semp.Module.Core.Data
{
    public class RepositoryQuery<T> : IRepositoryQuery<T>
       where T : class
    {
        protected DbContext Context { get; }

        protected DbQuery<T> DbQuery { get; }

        public RepositoryQuery(SempDbContext context) 
        {
            Context = context;
            DbQuery = Context.Query<T>();
        }

        public IQueryable<T> Query()
        {
            return DbQuery;
        }

    }
}
