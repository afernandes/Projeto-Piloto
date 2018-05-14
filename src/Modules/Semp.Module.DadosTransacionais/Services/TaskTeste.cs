using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Semp.Infrastructure.ScheduledTasks;

namespace Semp.Module.DadosTransacionais.Services
{
    public class TaskTeste : IScheduledTask
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;

        public TaskTeste(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }

        public string Schedule => "*/30 * * * * *";

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested) return;
            await Testando(stoppingToken);
        }

        private async Task Testando(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                System.Diagnostics.Debug.WriteLine("teste task " + DateTime.Now.ToString());
            });
        }
    }
}
