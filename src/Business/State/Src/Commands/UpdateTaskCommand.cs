using System;
using MediatR;
using Objects.Dto;

namespace State.Commands
{
    public class UpdateTaskCommand : IRequest<StateResult>
    {
        public ulong Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus? Status { get; set; }

        public DateTime? ExpirationUtc { get; set; }
    }
}
