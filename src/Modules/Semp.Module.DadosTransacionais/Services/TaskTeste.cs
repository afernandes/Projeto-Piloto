using Semp.Infrastructure.ScheduledTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Semp.Module.DadosTransacionais.Services
{
    public class TaskTeste : IScheduledTask
    {
        public string Schedule => "1 * * * *";

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.Print("teste task " + DateTime.Now.ToString());

            return Task.CompletedTask;
        }
    }
}
