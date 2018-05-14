using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Semp.Infrastructure.Data;
using Semp.Module.Core.Models;
using Microsoft.AspNetCore.Hosting;

namespace Semp.Module.Core.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/appsettings")]
    public class AppSettingApiController : Controller
    {
        private readonly IRepository<AppSetting> _appSettingRepository;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IApplicationLifetime _applicationLifetime;


        public AppSettingApiController(
            IRepository<AppSetting> appSettingRepository, 
            IConfiguration configuration,
            IApplicationLifetime appLifetime)
        {
            _appSettingRepository = appSettingRepository;
            _configurationRoot = (IConfigurationRoot)configuration;
            _applicationLifetime = appLifetime;
        }

        public async Task<IActionResult> Get()
        {
            var settings = await _appSettingRepository.Query().Where(x => x.IsVisibleInCommonSettingPage).ToListAsync();
            return Json(settings);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] IList<AppSetting> model)
        {
            if (ModelState.IsValid)
            {
                var settings = await _appSettingRepository.Query().Where(x => x.IsVisibleInCommonSettingPage).ToListAsync();
                foreach(var item in settings)
                {
                    var vm = model.FirstOrDefault(x => x.Key == item.Key);
                    if (vm != null)
                    {
                        item.Value = vm.Value;
                    }
                }

                await _appSettingRepository.SaveChangesAsync();
                _configurationRoot.Reload();

                return Accepted();
            }

            return BadRequest(ModelState);
        }

        [Route("api/appsettings/shutdown")]
        public IActionResult Post()
        {
            _applicationLifetime.StopApplication();
            return Accepted();
        }

    }
}
