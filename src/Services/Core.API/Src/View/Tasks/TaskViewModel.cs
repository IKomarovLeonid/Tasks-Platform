using System;
using Objects.Common;
using Objects.Dto;

namespace Core.API.View.Tasks
{
    public class TaskViewModel
    {
        public ulong Id { get; set; }

        public RootState State { get; set; }

        public string Title { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime? ExpirationUtc { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }
    }
}
