using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Objects.Common;

namespace Objects.Dto
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

   // [JsonConverter(typeof(StringEnumConverter))]
    public enum TaskStatus
    {
        NotDefined,
        Pending,
        Processing,
        Processed
    }
}
