using System.Collections.Generic;
using Semp.Module.Core.ViewModels;

namespace Semp.Module.Cms.ViewModels
{
    public class CarouselWidgetForm : WidgetFormBase
    {
        public IList<CarouselWidgetItemForm> Items = new List<CarouselWidgetItemForm>();
    }
}
