using System;
using Environment;
using Objects.Common;

namespace State
{
    public class StateResult : IAbstractResult
    {
        public ulong Id { get; private init; }

        public ErrorCode Code { get; private init; }

        public string ErrorMessage { get; private init; }

        public DateTime? ErrorTimeUtc { get; private init; }

        private StateResult() {}

        public static StateResult Applied(ulong id) => new StateResult()
        {
            Id = id,
            Code = ErrorCode.None,
            ErrorMessage = string.Empty,
            ErrorTimeUtc = DateTime.UtcNow,
        };

        public static StateResult Error(ErrorCode code, string message = null) => new StateResult()
        {
            Code = code,
            ErrorMessage = message ?? string.Empty,
            ErrorTimeUtc = DateTime.UtcNow,
        };
    }
}
