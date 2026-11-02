using MediatR;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Semp.Infrastructure
{
    public class SequentialMediator : Mediator
    {
        public SequentialMediator(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        protected async override Task PublishCore(IEnumerable<Func<Task>> allHandlers)
        {
            foreach (var handler in allHandlers)
            {
                await handler().ConfigureAwait(false);
            }
        }
    }
}
