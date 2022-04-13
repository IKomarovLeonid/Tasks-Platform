using System;
using TasksPlatform.Shared.API;

namespace Integration.Helpers
{
    internal class RequestsFactory
    {
        public static CreateTaskRequestModel DefaultCreateTaskRequest() => new CreateTaskRequestModel()
        {
            Title = "Default task",
            Description = "Default description",
            ExpirationUtc = DateTime.UtcNow.AddHours(2),
            Category = "Test category", 
            Priority = Priority.High
        };
    }
}
