using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Processing.Jobs;
using Quartz;

namespace Core.API.Quartz
{
    internal class QuartzService : IHostedService
    {
        private readonly IScheduler _scheduler;

        public QuartzService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var cachesJobDetail = JobBuilder.Create<CheckTaskExpirationJob>()
                .WithIdentity("CheckTaskExpirationJob", "group1")
                .Build();

            ITrigger cachesJobTrigger = TriggerBuilder.Create()
                .WithIdentity("jobsTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(cachesJobDetail, cachesJobTrigger);

            await _scheduler.Start();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler?.Shutdown();
        }
    }
}
