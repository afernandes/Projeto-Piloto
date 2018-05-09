using System.Linq;
using Semp.Module.Core.Models;

namespace Semp.Module.Core.Services
{
    public interface IWidgetInstanceService
    {
        IQueryable<WidgetInstance> GetPublished();
    }
}
