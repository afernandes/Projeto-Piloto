using System.Threading;
using System.Threading.Tasks;

namespace Semp.Infrastructure.ScheduledTasks
{
    public interface IScheduledTask
    {
        /*
        * * * * * *
        - - - - - -
        | | | | | |
        | | | | | +--- day of week (0 - 6) (Sunday=0)
        | | | | +----- month (1 - 12)
        | | | +------- day of month (1 - 31)
        | | +--------- hour (0 - 23)
        | +----------- min (0 - 59)
        +------------- sec (0 - 59)
        */
        string Schedule { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
