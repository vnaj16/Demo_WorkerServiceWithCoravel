using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel;

namespace Demo_WorkerServiceWithCoravel
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();

            host.Services.UseScheduler(scheduler =>
            {
                var jobSchedule = scheduler.Schedule<MyTaskVNAJ>();
                jobSchedule.EverySeconds(5)
                //.PreventOverlapping("myTaskVNAJ")
                ;
            });

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(builder =>
                    {
                        builder
                        .AddSerilog(new LoggerConfiguration().WriteTo.File(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @$"\serilog-{DateTime.Now.Date.ToShortDateString().Replace('/','-')}.txt").CreateLogger())
                        .AddConsole();
                    });
                    //services.AddHostedService<Worker>();
                    services.AddScheduler();
                    services.AddTransient<MyTaskVNAJ>();
                });
    }
}
