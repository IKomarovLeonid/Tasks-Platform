using System.Collections.Generic;
using Objects.Common;

namespace Environment.Queries
{
    public class SelectResult<TModel> : IAbstractResult
    {
        public ErrorCode Code { get; private init; }

        public string Message { get; private init; }

        public ICollection<TModel> Data { get; private init; }

        private SelectResult(){}

        public static SelectResult<TModel> Fetched(ICollection<TModel> data) => new SelectResult<TModel>()
        {
            Data = data,
            Code = ErrorCode.None
        };

        public static SelectResult<TModel> Error(ErrorCode code, string message = " ") => new SelectResult<TModel>()
        {
            Code = code,
            Message = message
        };
    }
}
