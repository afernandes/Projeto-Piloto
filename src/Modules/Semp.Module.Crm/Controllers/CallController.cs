using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Semp.Module.Crm.Controllers
{
    public class CallController : Controller
    {
        // GET: Call
        public ActionResult Index()
        {
            // Find your Account Sid and Auth Token at twilio.com/console
            const string accountSid = "ACcb910fbb0ec04abb39dbe4811ec2bd5b";
            const string authToken = "1ef71c03a18e12f0935dee852882277e";
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber("+5511998206459");
            var from = new PhoneNumber("+554139075303");
            var call = CallResource.Create(to, from,
                url: new Uri("http://demo.twilio.com/docs/voice.xml"));

            return View();
        }

        // GET: Call/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Call/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Call/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Call/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Call/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Call/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Call/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}