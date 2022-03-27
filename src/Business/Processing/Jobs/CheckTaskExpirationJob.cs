using System;
using System.Threading.Tasks;
using Objects.Common;
using Objects.Dto;
using Persistence.Storage;
using Quartz;
using TaskStatus = Objects.Dto.TaskStatus;

namespace Processing.Jobs
{
    public class CheckTaskExpirationJob : IJob
    {
        // services
        private readonly IStorage<TaskDto> _storage;

        public CheckTaskExpirationJob(IStorage<TaskDto> storage)
        {
            _storage = storage;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var tasks = await _storage.GetAllAsync(t => t.State == RootState.Active && t.ExpirationUtc.HasValue);

            if (tasks.Count == 0) return;

            Console.WriteLine($"Job checks '{tasks.Count}' tasks for expiration");

            foreach (var task in tasks)
            {
                Console.WriteLine($"Job checks #{task.Id} expiration...");

                var time = DateTime.UtcNow;

                if (task.ExpirationUtc.Value <= time && task.Status != TaskStatus.Expired)
                {
                    Console.WriteLine($"Task #{task.Id} has been expired (current time: {time}), (expiration: {task.ExpirationUtc}");
                    task.Status = TaskStatus.Expired;
                    await _storage.UpdateAsync(task);
                    continue;
                }
                Console.WriteLine($"Task #{task.Id} does not expired");
            }

            Console.WriteLine($"Job has checked '{tasks.Count}' tasks for expiration");
        }
    }
}
