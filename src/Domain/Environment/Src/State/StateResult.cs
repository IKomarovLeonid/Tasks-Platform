using System;
using Objects.Common;

namespace Environment.State
{
    public class StateResult : IAbstractResult
    {
        public ulong? Id { get; private init; }

        public ErrorCode Code { get; private init; }

        public string Message { get; private init; }

        public DateTime? ErrorTimeUtc { get; private init; }

        private StateResult() {}

        public static StateResult Applied(ulong id) => new StateResult()
        {
            Id = id,
            Code = ErrorCode.None,
            Message = string.Empty,
        };

        public static StateResult Applied() => new StateResult()
        {
            Code = ErrorCode.None,
            Message = string.Empty
        };

        public static StateResult Error(ErrorCode code, string message = null) => new StateResult()
        {
            Code = code,
            Message = message ?? string.Empty,
            ErrorTimeUtc = DateTime.UtcNow,
        };
    }
}
