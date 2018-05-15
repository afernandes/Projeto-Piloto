using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Semp.Infrastructure;
using Semp.Infrastructure.ScheduledTasks;
using Semp.Module.DadosTransacionais.Services;

namespace Semp.Module.DadosTransacionais
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IOrderSapService, OrderSapService>();
            //serviceCollection.AddSingleton<IHostedService, OrderDownloadBackgroundService>();
            serviceCollection.AddSingleton<IScheduledTask, OrderSapToLegacyBackgroundService>();            
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
        }
    }
}
