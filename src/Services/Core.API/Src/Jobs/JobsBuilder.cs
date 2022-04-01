using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Objects.Settings;
using Persistence.Storage;
using Processing.Jobs;
using Quartz;
using Scheduler.Src;

namespace Core.API.Jobs
{
    public class JobsBuilder : IJobsBuilder
    {
        private readonly ISettingsStorage<BaseSettings> _storage;

        public JobsBuilder(ISettingsStorage<BaseSettings> storage)
        {
            _storage = storage;
        }

        public async Task<ICollection<JobDescription>> BuildJobs()
        {
            var settings = await _storage.FindAsync(SettingsType.Jobs.ToString());

            if (settings == null) throw new Exception("Unable to register jobs. No settings has been found");

            var jobSettings = JsonConvert.DeserializeObject<JobSettings>(settings.Value);

            var jobs = new List<JobDescription>();

            var jobDetail = JobBuilder.Create<CheckTaskExpirationJob>()
                .WithIdentity("CheckTaskExpirationJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("jobsTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(jobSettings.CheckTaskExpirationJobSec)
                    .RepeatForever())
                .Build();

            jobs.Add(new JobDescription(trigger, jobDetail));

            return jobs;
        } 
    }
}
