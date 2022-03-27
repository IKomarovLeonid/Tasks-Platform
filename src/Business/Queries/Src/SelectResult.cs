using System;
using System.Collections.Generic;
using Objects.Common;

namespace Queries
{
    public class SelectResult<TModel>
    {
        public ErrorCode ErrorCode { get; private init; }

        public DateTime TimeUtc { get; private init; }

        public ICollection<TModel> Data { get; private init; }

        private SelectResult(){}

        public static SelectResult<TModel> Fetched(ICollection<TModel> data) => new SelectResult<TModel>()
        {
            Data = data,
            ErrorCode = ErrorCode.None,
            TimeUtc = DateTime.UtcNow
        };

        public static SelectResult<TModel> Error(ErrorCode code) => new SelectResult<TModel>()
        {
            ErrorCode = code,
            TimeUtc = DateTime.UtcNow
        };
    }
}
