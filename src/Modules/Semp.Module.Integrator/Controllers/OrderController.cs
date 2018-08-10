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


        [HttpGet("integrator/order-errors")]
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
    }
}