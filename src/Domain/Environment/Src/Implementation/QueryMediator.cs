using System;
using System.Threading.Tasks;
using Environment.Queries;
using MediatR;
using Newtonsoft.Json;
using NLog;
using Objects.Common;

namespace Environment.Implementation
{
    public class QueryMediator : IQueryMediator
    {
        private readonly IMediator _mediator;

        private static readonly ILogger _logger = LogManager.GetLogger(nameof(QueryMediator));


        public QueryMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<FindResult<TModel>> FindAsync<TModel>(FindQuery<TModel> query)
        {
            try
            {
                _logger.Info(JsonConvert.SerializeObject(query));

                var response = await _mediator.Send(query);

                _logger.Info(JsonConvert.SerializeObject(response));

                return response;
            }
            catch(Exception ex)
            {
                return FindResult<TModel>.Error(ErrorCode.InternalError, message: ex.Message);
            }
        }

        public async Task<SelectResult<TModel>> SelectAsync<TModel>(SelectQuery<TModel> query)
        {
            try
            {
                _logger.Info(JsonConvert.SerializeObject(query));

                var response = await _mediator.Send(query);

                _logger.Info(JsonConvert.SerializeObject(response));

                return response;
            }
            catch(Exception ex)
            {
                return SelectResult<TModel>.Error(ErrorCode.InternalError, message: ex.Message);
            }
        }
    }
}
