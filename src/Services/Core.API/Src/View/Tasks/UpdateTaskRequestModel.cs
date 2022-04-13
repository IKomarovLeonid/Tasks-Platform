using System;
using Objects.Dto;

namespace Core.API.View.Tasks
{
    public class UpdateTaskRequestModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus? Status { get; set; }

        public DateTime? ExpirationUtc { get; set; }

        public string Category { get; set; }

        public Priority? Priority { get; set; }
    }
}
