using System.Collections.Generic;
using System.Threading.Tasks;
using Semp.Module.Core.ViewModels;

namespace Semp.Module.Core.Services
{
    public interface IThemeService
    {
        Task<IList<ThemeListItem>> GetInstalledThemes();

        Task SetCurrentTheme(string themeName);
    }
}
