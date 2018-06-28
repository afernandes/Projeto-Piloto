using System.Collections.Generic;
using Semp.Module.Core.ViewModels;

namespace Semp.Module.Cms.ViewModels
{
    public class SpaceBarWidgetForm : WidgetFormBase
    {
        public IList<SpaceBarWidgetSetting> Items = new List<SpaceBarWidgetSetting>();
    }
}
