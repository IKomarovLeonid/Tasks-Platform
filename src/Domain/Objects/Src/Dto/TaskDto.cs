using System;
using Objects.Src.Common;

namespace Objects.Src.Dto
{
    public class TaskDto
    {
        public ulong Id { get; set; }

        public RootState State { get; set; }

        public string Title { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime ExpirationUtc { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }
    }

    public enum TaskStatus
    {
        NotDefined,
        Pending,
        Processing,
        Processed
    }
}
