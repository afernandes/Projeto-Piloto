using System.Threading.Tasks;
using Semp.Module.Core.Models;


namespace Semp.Module.Core.Extensions
{
    public interface IWorkContext
    {
        Task<User> GetCurrentUser();
    }
}
