﻿using System.Threading.Tasks;

namespace Semp.Module.Core.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
