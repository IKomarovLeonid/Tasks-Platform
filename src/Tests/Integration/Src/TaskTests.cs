using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

            _client.BaseAddress = new Uri("http://localhost:8080");

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
            var response = await _client.GetAsync("/api/ping");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task User_CanCreateTask_Success()
        {
            var data = new Dictionary<string, string>
            {
                {"title", "Tests task"},
                {"description", "My task for research" }
            };


            var response = await _client.PostAsync("/api/tasks", new FormUrlEncodedContent(data));

            var responseData = await response.Content.ReadAsStreamAsync();
            
        }

        [Test]
        public void User_CanViewTask_Success()
        {
            
        }

        [Test]
        public async Task User_CanViewAllTasks_Success()
        {
            // arrange 
            var data = new Dictionary<string, string>
            {
                {"title", "Tests task"},
                {"description", "My task for research" }
            };

            await _client.PostAsync("/api/tasks", new FormUrlEncodedContent(data));

            // act
            var response = await _client.GetAsync("api/tasks");

            // assert
        }

        [Test]
        public async Task User_CanUpdateTask_Success()
        {
            // arrange 
            var data = new Dictionary<string, string>
            {
                {"title", "Tests task"},
                {"description", "My task for research" }
            };

            var affected = await _client.PostAsync("/api/tasks", new FormUrlEncodedContent(data));

            // act
            var updateRequest = new Dictionary<string, string>
            {
                {"title", "Tests task"},
                {"description", "My task for research" },
                {"id", "1"}
            };

            var response = await _client.PatchAsync("api/tasks", new FormUrlEncodedContent(updateRequest));

            // assert
        }

        [Test]
        public async Task User_CanArchiveTask_Success()
        {
            // arrange 
            var data = new Dictionary<string, string>
            {
                {"title", "Tests task"},
                {"description", "My task for research" }
            };

            var affected = await _client.PostAsync("/api/tasks", new FormUrlEncodedContent(data));

            // act
            var response = await _client.DeleteAsync("api/tasks/{1}");

            // assert


        }
    }
}
