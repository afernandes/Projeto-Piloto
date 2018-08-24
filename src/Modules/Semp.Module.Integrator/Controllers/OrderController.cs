using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semp.Infrastructure.Data;
using Semp.Module.Core.Extensions;
using Semp.Module.Core.Services;
using Semp.Module.Integrator.Models;
using Semp.Module.Integrator.ViewModels;
using Semp.Module.Integrator.Data;

namespace Semp.Module.Integrator.Controllers
{
    [Authorize]
    [Route("integrator/order")]
    public class OrderController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IOrderRepository _orderRepository;
        private readonly IWorkContext _workContext;

        public OrderController(IOrderRepository orderRepository, IWorkContext workContext, IMediaService mediaService)
        {
            _orderRepository = orderRepository;
            _workContext = workContext;
            _mediaService = mediaService;
        }


        [HttpGet("send-errors")]
        public async Task<IActionResult> OrderErrorList()
        {
            var user = await _workContext.GetCurrentUser();

            var orders = await _orderRepository.List()
                .Select(x => new OrderDetailVm
                {
                    Id = x.Id,
                    OrderType = x.ORDER_TYPE,
                    OrderLegacy = x.PEDIDO_LEGADO,
                    Attendence = x.LOTE_ATENDIMENTO,
                    UpdateTimeSap = x.UPDATE_TIME_SAP,
                    Error = x.ERRO,
                    DocumentNumber = x.CNPJ,
                    ClientName = x.Cliente,
                    Selected = false
                })
                .OrderByDescending(x => x.UpdateTimeSap).ToListAsync();

            var model = new OrderSelecionViewModel();
            model.Order.AddRange(orders);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSelected(OrderSelecionViewModel model)
        {
            // get the ids of the items selected:
            var selectedIds = model.getSelectedIds();

            // Use the ids to retrieve the records for the selected people
            // from the database:
            var selectedOrder = await _orderRepository.List().Where(x => selectedIds.Contains(x.Id)).ToListAsync();

            // Process according to your requirements:
            foreach (var order in selectedOrder)
            {
                Resend(order.Id.Value);
            }

            // Redirect somewhere meaningful (probably to somewhere showing 
            // the results of your processing):
            return RedirectToAction("OrderErrorList");
        }

        [HttpGet("resend")]
        public ActionResult Resend(Guid id)
        {
            _orderRepository.ResendOrder(id);

            return RedirectToAction("OrderErrorList");
        }

    }
}