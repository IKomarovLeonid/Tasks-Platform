using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Integration.Helpers;
using NUnit.Framework;

namespace Integration
{
    public class SettingsTests : BaseTests
    {
        [Test]
        public async Task User_CanSet_JobSettings()
        {
            // arrange
            const int time = 10;

            var data = new Dictionary<string, string>
            {
                {"checkTaskExpirationJobSec", $"{time}"}
            };

            // act
            var response = await Client.PostDataAsync("settings/jobs", data);

            var model = await Client.GetAsync($"settings/jobs");

            var tasks = await model.Content.ReadAsStringAsync();

            var job = ResponseHelper.GetDataFromResponse<int>(tasks, "checkTaskExpirationJobSec");

            // assert
            Assert.That(job, Is.EqualTo(time));
        }
    }
}
