using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.API.View;
using Microsoft.AspNetCore.Mvc;
using Objects.Common;
using TaskStatus = Objects.Dto.TaskStatus;

namespace Core.API.Controllers
{
    [ApiController, Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ICollection<TaskViewModel>>> GetAsync(VisibleScope scope)
        {
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TaskViewModel>> GetByIdAsync()
        {
            return new TaskViewModel()
            {
                Id = 1,
                Title = "Mock task",
                State = RootState.Active,
                Status = TaskStatus.Processing,
                ExpirationUtc = DateTime.Now.AddDays(1),
                CreatedUtc = DateTime.Now,
                UpdatedUtc = DateTime.Now
            };
        }
    }
}
