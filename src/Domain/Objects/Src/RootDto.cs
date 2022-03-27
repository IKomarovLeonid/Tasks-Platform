using System;
using Objects.Common;

namespace Objects
{
    public abstract class RootDto : IDto
    {
        public ulong Id { get; set; }

        public RootState State { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }
    }
}
