﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace Scheduler
{
    public class QuartzService : IHostedService
    {
        private readonly IScheduler _scheduler;
        private readonly IJobsBuilder _builder;

        public QuartzService(IScheduler scheduler, IJobsBuilder builder)
        {
            _scheduler = scheduler;
            _builder = builder;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var jobs = await _builder.BuildJobs();

            foreach (var job in jobs)
            {
                await _scheduler.ScheduleJob(job.JobDetail, job.Trigger, cancellationToken);   
            }

            await _scheduler.Start();
        }

        public async Task RestartJobsAsync()
        {
            await _scheduler?.Shutdown();

            var jobs = await _builder.BuildJobs();

            foreach (var job in jobs)
            {
                await _scheduler.ScheduleJob(job.JobDetail, job.Trigger);
            }

            await _scheduler.Start();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler?.Shutdown();
        }
    }
}