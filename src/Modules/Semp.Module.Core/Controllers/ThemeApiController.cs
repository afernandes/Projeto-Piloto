using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semp.Module.Core.Services;
using Semp.Module.Core.ViewModels;

namespace Semp.Module.Core.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/themes")]
    public class ThemeApiController : Controller
    {
        private readonly IThemeService _themeService;

        public ThemeApiController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        public async Task<IActionResult> Get()
        {
            var themes = await _themeService.GetInstalledThemes();
            return Json(themes);
        }

        [HttpPost("use-theme")]
        public async Task<IActionResult> Post([FromBody] ThemeListItem model)
        {
            if (ModelState.IsValid)
            {
                await _themeService.SetCurrentTheme(model.Name);

                return Accepted();
            }
            return BadRequest(ModelState);
        }
    }
}
