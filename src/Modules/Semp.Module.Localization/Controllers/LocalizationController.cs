using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Semp.Infrastructure.Data;
using Semp.Module.Core.Extensions;
using Semp.Module.Core.Models;

namespace Semp.Module.Localization.Controllers
{
    public class LocalizationController : Controller
    {
        private readonly IRepositoryWithTypedId<User, long> _userRepository;
        private readonly IWorkContext _workContext;

        public LocalizationController(IRepositoryWithTypedId<User, long> userRepository, IWorkContext workContext)
        {
            _userRepository = userRepository;
            _workContext = workContext;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            /*Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddYears(1),
                    Secure = false
                }
            );*/

            var currentUser = _userRepository.Query()
                .Single(u => u.Email == _workContext.GetCurrentUser().Result.Email);
            currentUser.Culture = culture;
            _userRepository.SaveChanges();

            return LocalRedirect(returnUrl);
        }
    }
}