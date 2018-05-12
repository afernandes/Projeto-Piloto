using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Semp.Infrastructure;
using Semp.Infrastructure.ScheduledTasks;
using Semp.Module.DadosTransacionais.Services;

namespace Semp.Module.DadosTransacionais
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddSingleton<IHostedService, OrderDownloadBackgroundService>();
            serviceCollection.AddSingleton<IScheduledTask, TaskTeste>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
        }
    }
}
