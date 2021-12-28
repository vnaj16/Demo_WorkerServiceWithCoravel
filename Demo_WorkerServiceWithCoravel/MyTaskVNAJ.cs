using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_WorkerServiceWithCoravel
{
    public class MyTaskVNAJ : IInvocable
    {
        ILogger logger = null;

        public MyTaskVNAJ(ILogger<MyTaskVNAJ> logger)
        {
            this.logger = logger;
        }

        public Task Invoke()
        {
            //Aca iria la logica que quiero que se ejecute cada cierto tiempo. Ojo con la vida del DI COntainer y eso
            var taskId = Guid.NewGuid();
            logger.LogInformation($"Starting task id {taskId} at {DateTime.Now} [Coravel]");
            return Task.FromResult(true);
        }
    }
}
