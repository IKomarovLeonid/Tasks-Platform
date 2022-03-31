using System;
using Environment.Src;
using MediatR;
using Objects.Dto;

namespace State.Commands.Tasks
{
    public class UpdateTaskCommand : IStateCommand
    {
        public ulong Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus? Status { get; set; }

        public DateTime? ExpirationUtc { get; set; }
    }
}
