using System;
using System.Linq;
using System.Threading.Tasks;
using Integration.Helpers;
using NUnit.Framework;
using TasksPlatform.Shared.API;
using TaskStatus = TasksPlatform.Shared.API.TaskStatus;

namespace Integration
{
    public class TasksTests : BaseTests
    {
        [Test]
        public async Task User_CanCreateTask_Success()
        {
            var request = RequestsFactory.DefaultCreateTaskRequest();

            var response = await Client.Tasks.CreateAsync(request);

            Assert.That(response.Id, Is.GreaterThan(0));
            Assert.That(response.TimeUtc, Is.GreaterThanOrEqualTo(DateTime.UtcNow.AddSeconds(-1)));
            Assert.That(response.TimeUtc, Is.LessThanOrEqualTo(DateTime.UtcNow));
        }

        [Test]
        public async Task User_CanViewTask_Success()
        {
            // arrange
            var request = RequestsFactory.DefaultCreateTaskRequest();
            var taskId = await Client.Tasks.CreateAsync(request);

            // act
            var taskModel = await Client.Tasks.GetByIdAsync(taskId.Id.Value);

            // assert
            Assert.That(taskModel.Title, Is.EqualTo(request.Title));
            Assert.That(taskModel.Description, Is.EqualTo(request.Description));
            Assert.That(taskModel.Status, Is.EqualTo(TaskStatus.Pending));
            Assert.That(taskModel.State, Is.EqualTo(RootState.Active));
            Assert.That(taskModel.CreatedUtc, Is.GreaterThan(DateTime.UtcNow.AddSeconds(-2)));
            Assert.That(taskModel.UpdatedUtc, Is.GreaterThan(DateTime.UtcNow.AddSeconds(-2)));
            Assert.That(taskModel.ExpirationUtc, Is.EqualTo(request.ExpirationUtc).Within(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public async Task User_CanViewAllTasks_Success()
        {
            // arrange
            var request = RequestsFactory.DefaultCreateTaskRequest();
            var taskId1 = await Client.Tasks.CreateAsync(request);
            var taskId2 = await Client.Tasks.CreateAsync(request);

            // act
            var page = await Client.Tasks.GetAsync(VisibleScope.Active);
            var tasks = page.Items;

            // assert
            Assert.That(tasks.Count(t => t.Id == taskId1.Id.Value), Is.EqualTo(1));
            Assert.That(tasks.Count(t => t.Id == taskId2.Id.Value), Is.EqualTo(1));
        }

        [Test]
        public async Task User_CanUpdateTask_Success()
        {
            // arrange
            var request = RequestsFactory.DefaultCreateTaskRequest();
            var task = await Client.Tasks.CreateAsync(request);

            var patchRequest = new UpdateTaskRequestModel()
            {
                Title = "Task updated",
                Description = "Task description updated",
                Status = TaskStatus.Processing
            };

            // act
            await Client.Tasks.PatchAsync(task.Id.Value, patchRequest);

            var taskModel = await Client.Tasks.GetByIdAsync(task.Id.Value);

            // assert
            Assert.That(taskModel.Title, Is.EqualTo(patchRequest.Title));
            Assert.That(taskModel.Description, Is.EqualTo(patchRequest.Description));
            Assert.That(taskModel.Status, Is.EqualTo(patchRequest.Status));
            Assert.That(taskModel.State, Is.EqualTo(RootState.Active));
            Assert.That(taskModel.CreatedUtc, Is.GreaterThan(DateTime.UtcNow.AddSeconds(-2)));
            Assert.That(taskModel.UpdatedUtc, Is.GreaterThan(DateTime.UtcNow.AddSeconds(-2)));
            Assert.That(taskModel.ExpirationUtc, Is.EqualTo(request.ExpirationUtc).Within(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public async Task User_CanArchiveTask_Success()
        {
            // arrange
            var request = RequestsFactory.DefaultCreateTaskRequest();
            var task = await Client.Tasks.CreateAsync(request);

            // act
            await Client.Tasks.ArchiveAsync(task.Id.Value);

            var taskModel = await Client.Tasks.GetByIdAsync(task.Id.Value);

            Assert.That(taskModel.State, Is.EqualTo(RootState.Archived));

        }

        [Test]
        public async Task User_CanViewArchivedTask()
        {
            // arrange
            var request = RequestsFactory.DefaultCreateTaskRequest();
            var task = await Client.Tasks.CreateAsync(request);
            await Client.Tasks.ArchiveAsync(task.Id.Value);

            // act
            var tasks = await Client.Tasks.GetAsync(VisibleScope.All);
            var model = tasks.Items.FirstOrDefault(t => t.Id == task.Id.Value);

            // assert
            Assert.NotNull(model);

        }

        [Test]
        public async Task System_ApplyExpirationCheck()
        {
            // arrange
            await Client.Settings.SetJobSettingsAsync(new JobSettings()
            {
                CheckTaskExpirationJobSec = 20,
                ReloadCachesJobSec = 50
            });

            var request = RequestsFactory.DefaultCreateTaskRequest();
            request.ExpirationUtc = DateTime.UtcNow.AddMinutes(1);
            var task = await Client.Tasks.CreateAsync(request);

            // act
            await Task.Delay(TimeSpan.FromSeconds(70));
            var model = await Client.Tasks.GetByIdAsync(task.Id.Value);

            // assert
            Assert.That(model.Status, Is.EqualTo(TaskStatus.Expired));
        }
    }
}
