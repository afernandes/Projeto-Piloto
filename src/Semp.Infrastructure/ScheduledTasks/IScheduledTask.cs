using System.Threading;
using System.Threading.Tasks;

namespace Semp.Infrastructure.ScheduledTasks
{
    public interface IScheduledTask
    {
        string Schedule { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
