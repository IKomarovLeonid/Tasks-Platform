using Objects.Common;

namespace Queries
{
    public class FindResult<TModel>
    {
        public TModel Data { get; private init; }

        public ErrorCode ErrorCode { get; private init; }

        private FindResult(){}

        public static FindResult<TModel> Ok(TModel data) => new()
        {
            Data = data,
            ErrorCode = ErrorCode.None
        };

        public static FindResult<TModel> Error(ErrorCode code) => new()
        {
            Data = default,
            ErrorCode = code
        };
    }
}
