using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

namespace Semp.Module.Crm.Controllers
{
    public class VoiceController : TwilioController
    {    
        [HttpPost]
        public TwiMLResult Index(VoiceRequest request)
        {
            var response = new VoiceResponse();
            response.Say($"Obrigado pela sua chamada! Tenha um ótimo dia. {request.FromCity}", Say.VoiceEnum.Alice, language: Say.LanguageEnum.PtBr);

            return TwiML(response);
        }
    }
}