using System;
using System.Threading.Tasks;
using Core.API.Mapping;
using Core.API.View;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Objects.Settings;
using Queries.Find;
using State.Commands.Settings;

namespace Core.API.Controllers
{
    [ApiController, Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IViewMapper _viewMapper;

        public SettingsController(IMediator mediator, IViewMapper mapper)
        {
            _mediator = mediator;
            _viewMapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<JobSettings>> GetJobSettingsAsync()
        {
            var result = await _mediator.Send(new FindQuery<JobSettings>());

            return result.Data;
        }

        [HttpPost]
        public async Task<ActionResult<AffectionViewModel>> SetJobSettingsAsync(JobSettings model)
        {
            var result = await _mediator.Send(new SetJobsCommand()
            {
                CheckTaskExpirationJobSec = model.CheckTaskExpirationJobSec
            });

            return _viewMapper.ToView(result);
        }
    }
}
