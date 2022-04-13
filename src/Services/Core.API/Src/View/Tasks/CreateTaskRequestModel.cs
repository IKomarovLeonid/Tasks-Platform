using Objects.Dto;
using System;

namespace Core.API.View.Tasks
{
    public class CreateTaskRequestModel
    {
        public string Title { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public DateTime? ExpirationUtc { get; set; }

        public Priority Priority { get; set; }
    }
}
