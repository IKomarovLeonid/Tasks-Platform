using System;
using System.Diagnostics;
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

        private static readonly ILogger Logger = LogManager.GetLogger(nameof(QueryMediator));

        private readonly Stopwatch _stopwatch = new Stopwatch();

        public QueryMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<FindResult<TModel>> FindAsync<TModel>(FindQuery<TModel> query)
        {
            try
            {
                Logger.Info($"Execute find query: {JsonConvert.SerializeObject(query)}");

                _stopwatch.Start();

                var response = await _mediator.Send(query);

                _stopwatch.Stop();

                Logger.Info($"Response (elapsed: {_stopwatch.ElapsedMilliseconds} milliseconds) of find query: {JsonConvert.SerializeObject(response)}");

                _stopwatch.Reset();

                return response;
            }
            catch(Exception ex)
            {
                _stopwatch.Reset();
                return FindResult<TModel>.Error(ErrorCode.InternalError, message: ex.Message);
            }
        }

        public async Task<SelectResult<TModel>> SelectAsync<TModel>(SelectQuery<TModel> query)
        {
            try
            {
                Logger.Info($"Execute select query: {JsonConvert.SerializeObject(query)}");

                _stopwatch.Start();

                var response = await _mediator.Send(query);

                _stopwatch.Stop();

                Logger.Info($"Response (elapsed: {_stopwatch.ElapsedMilliseconds} milliseconds) of select query [Errors: {response.Code}]");

                _stopwatch.Reset();

                return response;
            }
            catch(Exception ex)
            {
                _stopwatch.Reset();
                return SelectResult<TModel>.Error(ErrorCode.InternalError, message: ex.Message);
            }
        }
    }
}
