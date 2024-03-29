﻿using System.Threading.Tasks;
using Core.API.Mapping;
using Core.API.View;
using Core.API.View.Tasks;
using Environment;
using Environment.Queries;
using Microsoft.AspNetCore.Mvc;
using Objects.Common;
using Objects.Dto;
using State.Commands.Tasks;

namespace Core.API.Controllers
{
    [ApiController, Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly IDomainMediator _mediator;
        private readonly IQueryMediator _queryMediator;
        private readonly IViewMapper _viewMapper;

        public TasksController(IDomainMediator mediator, IQueryMediator queryMediator, IViewMapper mapper)
        {
            _mediator = mediator;
            _queryMediator = queryMediator;
            _viewMapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PageViewModel<TaskViewModel>>> GetAsync(VisibleScope scope)
        {
            var result = await _queryMediator.SelectAsync(new SelectQuery<TaskDto>(scope));

            return _viewMapper.ToView<TaskDto, TaskViewModel>(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TaskViewModel>> GetByIdAsync(ulong id)
        {
            var result = await _queryMediator.FindAsync(new FindQuery<TaskDto>(id));

            return _viewMapper.ToView<TaskDto, TaskViewModel>(result);
        }


        [HttpPost]
        public async Task<ActionResult<AffectionViewModel>> CreateAsync([FromBody] CreateTaskRequestModel request)
        {
            var result = await _mediator.SendAsync(new CreateTaskCommand()
            {
                Title = request.Title,
                Description = request.Description,
                ExpirationUtc = request.ExpirationUtc,
                Category = request.Category,
                Priority = request.Priority
            });

            return _viewMapper.ToView(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<AffectionViewModel>> PatchAsync(ulong id, [FromBody] UpdateTaskRequestModel request)
        {
            var result = await _mediator.SendAsync(new UpdateTaskCommand()
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                ExpirationUtc = request.ExpirationUtc,
                Category = request.Category,
                Priority = request.Priority
            });

            return _viewMapper.ToView(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AffectionViewModel>> ArchiveAsync(ulong id)
        {
            var result = await _mediator.SendAsync(new ArchiveTaskCommand(id));

            return _viewMapper.ToView(result);
        }
    }
}
