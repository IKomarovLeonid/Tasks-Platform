using System;
using Objects.Common;

namespace Core.API.View
{
    class ErrorViewResponse
    {
        public string Message { get; private init; }

        public ErrorCode ErrorCode { get; private init; }

        public DateTime ErrorTimeUtc { get; private init; }

        public static ErrorViewResponse Build(ErrorCode code, string message = null) => new()
        {
            ErrorCode = code,
            Message = message,
            ErrorTimeUtc = DateTime.UtcNow
        };
    }
}
