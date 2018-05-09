using System.Threading.Tasks;
using Semp.Module.Cms.Models;

namespace Semp.Module.Cms.Services
{
    public interface IPageService
    {
        Task Create(Page page);

        Task Update(Page page);

        Task Delete(Page page);
    }
}
