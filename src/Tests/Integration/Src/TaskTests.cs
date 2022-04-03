using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Integration.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Integration
{
    public class TasksTests
    {
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            _client = new HttpClient();

            _client.BaseAddress = new Uri("http://localhost:8080/api/");

            _client.DefaultRequestHeaders.Add("accept", "application/json");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
        }

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            var response = await _client.GetAsync("ping");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task User_CanCreateTask_Success()
        {
            var title = "Tests task";
            var description = "My task for research";
            var time = DateTime.UtcNow.AddHours(5);

            var data = new Dictionary<string, string>
            {
                {"title", title},
                {"description", description },
                {"ExpirationUtc", $"{time}"}
            };

            var response = await _client.PostDataAsync("tasks", data);

            var responseData = await response.Content.ReadAsStringAsync();

            var id = ResponseHelper.GetDataFromResponse<int>(responseData, "id");
            var affected = ResponseHelper.GetDataFromResponse<DateTime>(responseData, "timeUtc");

            Assert.That(id, Is.GreaterThan(0));
            Assert.That(affected, Is.GreaterThanOrEqualTo(DateTime.UtcNow.AddSeconds(-1)));
            Assert.That(affected, Is.LessThanOrEqualTo(DateTime.UtcNow));
            
        }

        [Test]
        public async Task User_CanViewTask_Success()
        {
            // arrange
            var title = "Tests task";
            var description = "My task for research";
            var time = DateTime.UtcNow.AddHours(5);

            var data = new Dictionary<string, string>
            {
                {"title", title},
                {"description", description },
                {"ExpirationUtc", $"{time}"}
            };

            var response = await _client.PostDataAsync("tasks", data);

            var responseData = await response.Content.ReadAsStringAsync();

            var id = ResponseHelper.GetDataFromResponse<int>(responseData, "id");

            // act
            var model = await _client.GetAsync($"tasks/{id}");

            var taskModel = await model.Content.ReadAsStringAsync();
            
            var titleActual = ResponseHelper.GetDataFromResponse<string>(taskModel, "title");
            var descriptionActual = ResponseHelper.GetDataFromResponse<string>(taskModel, "description");
            var expirationActual = ResponseHelper.GetDataFromResponse<DateTime>(taskModel, "expirationUtc");
            var state = ResponseHelper.GetDataFromResponse<string>(taskModel, "state");

            // assert
            Assert.That(titleActual, Is.EqualTo(title));
            Assert.That(descriptionActual, Is.EqualTo(description));
            Assert.That(expirationActual, Is.EqualTo(time).Within(TimeSpan.FromSeconds(1)));
            Assert.That(state, Is.EqualTo("Active"));
        }

        [Test]
        public async Task User_CanViewAllTasks_Success()
        {
            // arrange
            var taskId1 = await _client.PostDefaultTaskAsync();
            var taskId2 = await _client.PostDefaultTaskAsync();

            // act
            var model = await _client.GetAsync($"tasks/");

            var tasks = await model.Content.ReadAsStringAsync();

            var items = ResponseHelper.GetDataFromResponse<string>(tasks, "items");

            // assert
            Assert.That(items.Length, Is.GreaterThan(0));

        }

        [Test]
        public async Task User_CanUpdateTask_Success()
        {
            // arrange
            var title = "Updated title";
            var description = "My updated description";
            var time = DateTime.UtcNow.AddHours(6);
            var status = "Processing";

            var taskId = await _client.PostDefaultTaskAsync();

            var data = new Dictionary<string, string>
            {
                {"title", $"{title}"},
                {"ExpirationUtc", $"{time}"},
                {"description", $"{description}" },
                {"status", $"{status}"}
            };

            // act
            await _client.PatchDataAsync($"tasks/{taskId}", data);

            var model = await _client.GetAsync($"tasks/{taskId}");

            var taskModel = await model.Content.ReadAsStringAsync();

            var titleActual = ResponseHelper.GetDataFromResponse<string>(taskModel, "title");
            var descriptionActual = ResponseHelper.GetDataFromResponse<string>(taskModel, "description");
            var expirationActual = ResponseHelper.GetDataFromResponse<DateTime>(taskModel, "expirationUtc");
            var statusActual = ResponseHelper.GetDataFromResponse<string>(taskModel, "status");

            // assert
            Assert.That(titleActual, Is.EqualTo(title));
            Assert.That(descriptionActual, Is.EqualTo(description));
            Assert.That(expirationActual, Is.EqualTo(time).Within(TimeSpan.FromSeconds(1)));
            Assert.That(statusActual, Is.EqualTo(status));
        }

        [Test]
        public async Task User_CanArchiveTask_Success()
        {
            // arrange 
            var taskId = await _client.PostDefaultTaskAsync();

            // act
            await _client.DeleteAsync($"tasks/{taskId}");

            var model = await _client.GetAsync($"tasks/{taskId}");

            var taskModel = await model.Content.ReadAsStringAsync();

            // assert
            var state = ResponseHelper.GetDataFromResponse<string>(taskModel, "state");

            Assert.That(state, Is.EqualTo("Archived"));

        }
    }
}
