using System;
using System.Threading.Tasks;
using Semp.Infrastructure.Web;
using Semp.Module.Core.Models;
using Semp.Module.Core.Services;
using Semp.Module.Integrator.Models;

namespace Semp.Module.Integrator.Services
{
    public class OrderEmailService : IOrderEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IRazorViewRenderer _viewRender;
        public OrderEmailService(IEmailSender emailSender, IRazorViewRenderer viewRender)
        {
            _emailSender = emailSender;
            _viewRender = viewRender;
        }

        public async Task SendEmailToUser(User user, OrderSendErrorView order)
        {
            var emailBody = await _viewRender.RenderViewToStringAsync("/Modules/Semp.Module.Orders/Views/EmailTemplates/OrderEmailToCustomer.cshtml", order);
            var emailSubject = $"Order information #{order}";
            await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBody, true);
        }
    }
}
