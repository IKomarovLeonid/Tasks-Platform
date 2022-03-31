using System;
using System.Collections.Generic;
using Environment;
using Objects.Common;

namespace Queries.Select
{
    public class SelectResult<TModel> : IAbstractResult
    {
        public ErrorCode Code { get; private init; }

        public DateTime TimeUtc { get; private init; }

        public ICollection<TModel> Data { get; private init; }

        private SelectResult(){}

        public static SelectResult<TModel> Fetched(ICollection<TModel> data) => new SelectResult<TModel>()
        {
            Data = data,
            Code = ErrorCode.None,
            TimeUtc = DateTime.UtcNow
        };

        public static SelectResult<TModel> Error(ErrorCode code) => new SelectResult<TModel>()
        {
            Code = code,
            TimeUtc = DateTime.UtcNow
        };
    }
}
