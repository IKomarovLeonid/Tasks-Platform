﻿using System;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using NLog;
using Objects.Common;
using State;

namespace Environment.Src.Implementation
{
    public class DomainMediator : IDomainMediator
    {
        private readonly IMediator _mediator;

        private static readonly ILogger _logger = LogManager.GetLogger(nameof(DomainMediator));

        public DomainMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<StateResult> SendAsync(IStateCommand command)
        {
            try
            {
                _logger.Info(JsonConvert.SerializeObject(command));

                var response = await _mediator.Send(command);

                _logger.Info(JsonConvert.SerializeObject(response));

                return response;
            }
            catch(Exception ex)
            {
                return StateResult.Error(ErrorCode.NotFound, message: ex.Message);
            }
        }
    }
}
