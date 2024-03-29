﻿using System;
using System.Threading.Tasks;
using Environment.Events;
using NLog;
using Objects.Settings;
using Persistence.Storage;
using Scheduler;

namespace Processing.Listeners
{
    public class JobsListener : IListener
    {
        private readonly ISettingsStorage<BaseSettings> _storage;
        private readonly QuartzService _quartz;

        private IDisposable _subscription;

        private static readonly ILogger Logger = LogManager.GetLogger(nameof(JobsListener));

        public JobsListener(ISettingsStorage<BaseSettings> storage, QuartzService quartz)
        {
            _storage = storage;
            _quartz = quartz;
        }

        public void Start()
        {
            _subscription = _storage.Subscribe(ProcessEvents);
        }

        public void Stop()
        {
            _subscription?.Dispose();
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }

        private void ProcessEvents(StateEvent<BaseSettings> stateEvent)
        {
            if(stateEvent.Type == StateEventType.Update)
            {
                Logger.Info("Job settings will be restarted");

                Task.Run(() => OnUpdate(stateEvent))
                    .ContinueWith(t => Logger.Error("Failed to restart jobs"),
                    TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        private void OnUpdate(StateEvent<BaseSettings> stateEvent)
        {
            _quartz.RestartJobsAsync().GetAwaiter().GetResult();
        }
    }
}
