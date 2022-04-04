using System.Threading.Tasks;
using NUnit.Framework;
using TasksPlatform.Shared.API;

namespace Integration
{
    public class SettingsTests : BaseTests
    {
        [Test]
        public async Task User_CanSet_JobSettings()
        {
            // arrange
            const int time = 10;
            const int cachesTime = 50;

            var request = new JobSettings()
            {
                CheckTaskExpirationJobSec = time,
                ReloadCachesJobSec = cachesTime
            };

            // act
            await Client.Settings.SetJobSettingsAsync(request);

            var model = await Client.Settings.GetJobSettingsAsync();

            // assert
            Assert.That(model.CheckTaskExpirationJobSec, Is.EqualTo(time));
            Assert.That(model.ReloadCachesJobSec, Is.EqualTo(cachesTime));
        }
    }
}
