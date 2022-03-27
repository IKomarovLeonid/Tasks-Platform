using System;

namespace Objects.Dto
{
    public class TaskDto : RootDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime? ExpirationUtc { get; set; }
    }
    
    public enum TaskStatus
    {
        NotDefined,
        Pending,
        Processing,
        Processed
    }
}
