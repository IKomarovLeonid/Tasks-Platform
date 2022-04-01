using System;
using System.Threading.Tasks;
using Core.API.Mapping;
using Core.API.View;
using Environment;
using Environment.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Objects.Settings;
using State.Commands.Settings;

namespace Core.API.Controllers
{
    [ApiController, Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly IDomainMediator _mediator;
        private readonly IQueryMediator _queryMediator;
        private readonly IViewMapper _viewMapper;

        public SettingsController(IDomainMediator mediator, IQueryMediator queryMediator, IViewMapper mapper)
        {
            _mediator = mediator;
            _queryMediator = queryMediator;
            _viewMapper = mapper;
        }

        [HttpGet("jobs")]
        public async Task<ActionResult<JobSettings>> GetJobSettingsAsync()
        {
            var result = await _queryMediator.FindAsync<JobSettings>(new FindQuery<JobSettings>());

            return result.Data;
        }

        [HttpPost("jobs")]
        public async Task<ActionResult<AffectionViewModel>> SetJobSettingsAsync(JobSettings model)
        {
            var result = await _mediator.SendAsync(new SetJobsCommand()
            {
                CheckTaskExpirationJobSec = model.CheckTaskExpirationJobSec
            });

            return _viewMapper.ToView(result);
        }
    }
}
