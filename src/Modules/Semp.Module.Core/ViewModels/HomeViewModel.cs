using System.Collections.Generic;

namespace Semp.Module.Core.ViewModels
{
    public class HomeViewModel
    {
        public IList<WidgetInstanceViewModel> WidgetInstances { get; set; } = new List<WidgetInstanceViewModel>();
    }
}
