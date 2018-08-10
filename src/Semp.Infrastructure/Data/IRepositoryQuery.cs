using System.Linq;

namespace Semp.Infrastructure.Data
{
    public interface IRepositoryQuery<T> where T : class
    {
        IQueryable<T> Query();
    }
}
