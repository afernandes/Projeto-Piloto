using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Semp.Infrastructure.Web;
using Semp.Module.Core.Extensions;
using Semp.Module.Core.ViewModels;
using Semp.Module.Integrator.Data;
using Semp.Module.Integrator.ViewModels;

namespace Semp.Module.Integrator.Components
{
    public class OrderIssuesWidgetViewComponent : ViewComponent
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IWorkContext _workContext;

        public OrderIssuesWidgetViewComponent(
            IOrderRepository orderRepository,
            IWorkContext workContext)
        {
            _orderRepository = orderRepository;
            _workContext = workContext;

        }

        public async Task<IViewComponentResult> InvokeAsync(WidgetInstanceViewModel widgetInstance)
        {
            var model = new OrderIssuesWidgetComponentVm
            {
                Id = widgetInstance.Id,
                WidgetName = widgetInstance.Name
                //Items = JsonConvert.DeserializeObject<List<OrderIssuesWidgetSetting>>(widgetInstance.Data)
            };

            var rolesNames = _workContext.GetRolesForCurrentUser();

            model.Items = (await _orderRepository.List().ToListAsync())
                .GroupBy(x => x.ORDER_TYPE)
                .Select(group => new OrderIssuesWidgetPanelVm
                {
                    OrderType = group.Key,
                    Count = group.Count()
                })
                .Where(x => rolesNames.Contains(x.OrderType, StringComparer.InvariantCultureIgnoreCase))
                .OrderBy(x => x.OrderType).ToList();
            
            return View(this.GetViewPath(), model);
        }

    }
}
