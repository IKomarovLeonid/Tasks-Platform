﻿using System;
using System.Threading.Tasks;
using NLog;
using Objects.Common;
using Objects.Dto;
using Persistence.Src;
using Persistence.Storage;
using Quartz;
using TaskStatus = Objects.Dto.TaskStatus;

namespace Processing.Jobs
{
    public class CheckTaskExpirationJob : IJob
    {
        // services
        private readonly IDomainManager<TaskDto> _storage;

        private readonly ILogger _logger = LogManager.GetLogger(nameof(CheckTaskExpirationJob));

        public CheckTaskExpirationJob(IDomainManager<TaskDto> storage)
        {
            _storage = storage;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var tasks = await _storage.GetAllAsync(t => t.State == RootState.Active && t.ExpirationUtc.HasValue && t.Status != TaskStatus.Expired);

            if (tasks.Count == 0)
            {
                _logger.Info("There are no active tasks to expiration check");
                return;
            }

            _logger.Info($"Job checks '{tasks.Count}' tasks for expiration");

            foreach (var task in tasks)
            {
                _logger.Info($"Job checks #{task.Id} expiration...");

                var time = DateTime.UtcNow;

                if (task.ExpirationUtc.Value <= time && task.Status != TaskStatus.Expired)
                {
                    _logger.Warn($"Task #{task.Id} has been expired (current time: {time}), (expiration: {task.ExpirationUtc}");
                    task.Status = TaskStatus.Expired;
                    await _storage.UpdateAsync(task);
                    continue;
                }
                _logger.Info($"Task #{task.Id} (expiration utc: '{task.ExpirationUtc}') does not expired. Current utc time: '{time}' ");
            }

            _logger.Info($"Job has checked '{tasks.Count}' tasks for expiration");
        }
    }
}
