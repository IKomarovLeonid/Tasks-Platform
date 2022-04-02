using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Environment.State;
using MediatR;
using Newtonsoft.Json;
using NLog;
using Objects.Common;

namespace Environment.Implementation
{
    public class DomainMediator : IDomainMediator
    {
        private readonly IMediator _mediator;

        private static readonly ILogger Logger = LogManager.GetLogger(nameof(DomainMediator));

        private readonly Stopwatch _stopwatch = new Stopwatch();

        public DomainMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<StateResult> SendAsync(IStateCommand command)
        {
            try
            {
                Logger.Info($"Execute state command: {JsonConvert.SerializeObject(command)}");

                _stopwatch.Start();

                var response = await _mediator.Send(command);

                _stopwatch.Stop();

                Logger.Info($"Response (elapsed: {_stopwatch.ElapsedMilliseconds} milliseconds) of state command: {JsonConvert.SerializeObject(response)}");

                _stopwatch.Reset();

                return response;
            }
            catch(Exception ex)
            {
                _stopwatch.Reset();
                return StateResult.Error(ErrorCode.InternalError, message: ex.Message);
            }
        }
    }
}
