using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.API.Mapping;
using Core.API.View;
using Core.API.View.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Objects.Common;
using Objects.Dto;
using Queries;
using State;
using State.Commands;

namespace Core.API.Controllers
{
    [ApiController, Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IViewMapper _viewMapper;

        public TasksController(IMediator mediator, IViewMapper mapper)
        {
            _mediator = mediator;
            _viewMapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PageViewModel<TaskDto>>> GetAsync(VisibleScope scope)
        {
            var result = await _mediator.Send(new SelectQuery<TaskDto>(scope));

            return PageViewModel<TaskDto>.New(result.Data);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TaskViewModel>> GetByIdAsync(ulong id)
        {
            var result = await _mediator.Send(new FindQuery<TaskDto>(id));

            return _viewMapper.ToView<TaskDto, TaskViewModel>(result);
        }


        [HttpPost]
        public async Task<ActionResult<StateResult>> CreateAsync([FromBody] CreateTaskRequestModel request)
        {
            var result = await _mediator.Send(new CreateTaskCommand()
            {
                Title = request.Title,
                Description = request.Description,
                ExpirationUtc = request.ExpirationUtc
            });

            return result;
        }
    }
}
