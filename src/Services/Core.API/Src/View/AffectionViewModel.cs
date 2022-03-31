using System;

namespace Core.API.View
{
    public class AffectionViewModel
    {
        public ulong? Id { get; private init; }

        public DateTime TimeUtc { get; private init; }

        public static AffectionViewModel New() => new() { TimeUtc = DateTime.UtcNow };

        public static AffectionViewModel New(ulong? id) => new() { Id = id, TimeUtc = DateTime.UtcNow };
    }
}
