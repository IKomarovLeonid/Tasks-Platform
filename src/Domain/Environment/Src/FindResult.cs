using Environment;
using Objects.Common;

namespace Queries.Find
{
    public class FindResult<TModel> : IAbstractResult
    {
        public TModel Data { get; private init; }

        public ErrorCode Code { get; private init; }

        private FindResult(){}

        public static FindResult<TModel> Ok(TModel data) => new()
        {
            Data = data,
            Code = ErrorCode.None
        };

        public static FindResult<TModel> Error(ErrorCode code) => new()
        {
            Data = default,
            Code = code
        };
    }
}
